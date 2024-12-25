import {send} from "../utilities"
let Cartb = document.getElementById("Cartb")!;
let b = document.getElementById("b")!;
let b2 = document.getElementById("b2")!;
let userDiv = document.querySelector("#userDiv") as HTMLDivElement;
let usernameDiv = document.querySelector("#usernameDiv") as HTMLDivElement;
let red = document.getElementById("red")!;
let bordo = document.getElementById("bordo")!;


let userId = localStorage.getItem("UserID");

let FoundUser = false;
if (userId != null) 
{
  FoundUser = await send("FoundUser", userId) as boolean;
}

console.log(FoundUser);
console.log(userId);

if (FoundUser) 
{
  
  userDiv.style.display = "block";
  
    let username = await send("GetUserName", userId)
    usernameDiv.innerText = "Logged In as " + username;
 }

b.onclick=function(){
 location.href="index2.html";
}
b2.onclick = function () {
    location.href = "signup.html";

    
}
Cartb.onclick = function () {
  location.href = "Wishlist.html";
}
red.onclick = function () {
  location.href = "red.html";
}
bordo.onclick = function () {
  location.href = "bordo.html";
}