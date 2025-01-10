 import { send } from "../utilities";
let userId = localStorage.getItem("userId");
let pics = document.querySelectorAll(".pic") as NodeListOf<HTMLImageElement>;

let favorites = await send("getFavorites", userId) as boolean[];

for (let i = 0; i < favorites.length; i++) { 
    if (favorites[i]) { 
        pics[i].style.display = "block";
    }
}