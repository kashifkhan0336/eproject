import"./main-551a2c86.js";import{v as n}from"./index-3a16d620.js";document.getElementById("username").addEventListener("input",r);document.getElementById("password").addEventListener("input",s);document.getElementById("loginForm").addEventListener("submit",function(t){t.preventDefault(),a(),r(),s();const e=document.getElementsByClassName("error");Array.from(e).some(o=>o.textContent!=="")||console.log("form submitted")});function r(){const t=document.getElementById("username").value,e=document.getElementById("usernameError");n.isAlphanumeric(t)&&n.isLength(t,{min:3,max:20})?e.textContent="":e.textContent="Username must be alphanumeric and have a length between 3 and 20 characters."}function s(){const t=document.getElementById("password").value,e=document.getElementById("passwordError");n.isLength(t,{min:6})?e.textContent="":e.textContent="Password must be at least 6 characters long."}function a(){const t=document.getElementsByClassName("error");for(let e=0;e<t.length;e++)t[e].textContent=""}