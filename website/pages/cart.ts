 import { send } from "../utilities";
let UserID = localStorage.getItem("UserID");
let pics = document.querySelectorAll(".pic") as NodeListOf<HTMLImageElement>;
console.log(UserID);

let favorites = await send("getFavorites", UserID) as boolean[];

for (let i = 0; i < favorites.length; i++) { 
    if (favorites[i]) { 
        pics[i].style.display = "block";
    }
}
