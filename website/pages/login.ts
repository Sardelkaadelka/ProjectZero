import { send } from "../utilities";

let loginb = document.getElementById("loginb")!;
let UserNameInput = document.getElementById("UserNameInput") as HTMLInputElement;
let PasswordInput = document.getElementById("PasswordInput") as HTMLInputElement;



loginb.onclick= async function () {
    let[UserFound, UserID] = await send ("login",[UserNameInput.value,PasswordInput.value]) as [boolean,string];
    console.log("User Found"+ UserFound);
    if(UserFound){
        localStorage.setItem("UserID",UserID);
    }
}

