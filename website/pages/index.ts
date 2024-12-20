import {send} from "../utilities"

let b = document.getElementById("b")!;
let b2 = document.getElementById("b2")!;
let userDiv = document.querySelector("#userDiv") as HTMLDivElement;
let usernameDiv = document.querySelector("#usernameDiv") as HTMLDivElement;

let UserID = localStorage.getItem("UserID");

let FoundUser = false;
if (UserID != null) 
{
  FoundUser = await send("FoundUser", UserID) as boolean;
}

if (FoundUser) 
{
    
    let username = await send("GetUsername", UserID)
    usernameDiv.innerText = "Logged In as " + username;
 }

b.onclick=function(){
 location.href="index2.html";
}
b2.onclick = function () {
    location.href = "signup.html";

    
}
