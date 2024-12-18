import { send } from "../utilities";

let loginb = document.getElementById("loginb")!;
let UserNameInput = document.getElementById("UserNameInput") as HTMLInputElement;


loginb.onclick= async function () {
    let[UserFound, UserID] = await send ("login",[UserNameInput.value])
}
