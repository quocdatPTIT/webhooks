var registerButtion = document.getElementById("register");
//var statusLabel = document.getElementById("statusLable");
var webhookURI = document.getElementById("webhook");
var webhookType = document.getElementById("webhooktype");
var successBox = document.getElementById("alertSuccess");
var dangerBox = document.getElementById("alertDanger");
var dangerMessage = document.getElementById("dangerMessage");
var successMessage = document.getElementById("successMessage");

successBox.style.display = 'none';
dangerBox.style.display = 'none';

registerButtion.onclick = function () {
    successBox.style.display = 'none';
    dangerBox.style.display = 'none';

    if (webhookURI.value == "") {
        dangerMessage.innerHTML = "PLease Enter a URI";
        dangerBox.style.display = 'block';
    }
    else {
        (async () => {
            const rawResponse = await fetch('http://localhost:5000/api/WebhookSubscription/create-subscription', {
                method: 'post',
                body: JSON.stringify({ webhookUri: webhookURI.value, webhookType: webhookType.value }),
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            const content = await rawResponse.json();

            successMessage.innerHTML = "Webhook Registered please use secret: " + content.secret + " to validate inbound request";
            successBox.style.display = 'block';

            console.log(content);
        })();
    }
};