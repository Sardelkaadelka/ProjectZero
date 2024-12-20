import { send } from "../utilities";

let SignUpb = document.querySelector("#SignUpb") as HTMLButtonElement;
let UserNameInput = document.querySelector("#UserNameInput") as HTMLInputElement;
let PasswordInput = document.querySelector("#passwordInput") as HTMLInputElement;
let alreadyhaveacc = document.getElementById("alreadyhaveacc")!;

SignUpb.onclick = function () {
    send("signup", [UserNameInput.value, PasswordInput.value]);
    location.href = "index.html";
}
alreadyhaveacc.onclick = function () {
    location.href="login.html"
}
