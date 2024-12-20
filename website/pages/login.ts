import { send } from "../utilities";

let loginb = document.querySelector("#loginb") as HTMLButtonElement;
let UserNameInput = document.querySelector("#UserNameInput") as HTMLInputElement;
let PasswordInput = document.querySelector("#passwordInput") as HTMLInputElement;




loginb.onclick = async function () {
    let [UserFound, UserID] = await send("login", [UserNameInput.value, PasswordInput.value]) as [boolean, string];
    console.log("User Found: " + UserFound);
    if (UserFound) {
        localStorage.setItem("UserID", UserID);
        location.href = "index.html";
    }
}



