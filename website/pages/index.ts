import {send} from "../utilities"

let b = document.getElementById("b")!;
let b2 = document.getElementById("b2")!;
b.onclick=function(){
 location.href="login.html";
}
b2.onclick = function () {
    location.href = "signup.html";
}