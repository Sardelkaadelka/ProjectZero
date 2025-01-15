import { send } from "../utilities"
let Cartb = document.getElementById("Cartb")!;
let b = document.getElementById("b")!;
let b2 = document.getElementById("b2")!;
let logoutb = document.getElementById("logoutb")!;
let userDiv = document.querySelector("#userDiv") as HTMLDivElement;
let usernameDiv = document.querySelector("#usernameDiv") as HTMLDivElement;

let addToCartButtons = document.querySelectorAll(".add-to-cart") as NodeListOf<HTMLButtonElement>;


let UserID = localStorage.getItem("UserID");

let FoundUser = false;
if ( UserID!= null) {
  FoundUser = await send("FoundUser", UserID) as boolean;
}

console.log(FoundUser);
console.log(UserID);

if (FoundUser) {
  userDiv.style.display = "block";
  Cartb.style.display="table-row";

  let username = await send("GetUserName", UserID)
  usernameDiv.innerText = "Logged In as " + username;
}
else{
  Cartb.style.display="none";
}

b.onclick = function () {
  location.href = "index2.html";
}
b2.onclick = function () {
  location.href = "signup.html";
}
logoutb.onclick = async function () {
  localStorage.removeItem("UserID");

  location.reload();
}

Cartb.onclick = function () {
  location.href = "Wishlist.html";
}
for (let i = 0; i < 3; i++) {
  addToCartButtons[i].onclick = function () {
    if (addToCartButtons)
      send("addtocart", [i, UserID])
  }
}