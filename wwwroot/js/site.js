import "flowbite"  
import Bowser from "bowser" 
import { initFlowbite } from "flowbite"
import * as signalR from '@microsoft/signalr'

let dotNet = null
let connection = new signalR.HubConnectionBuilder().withUrl("/_chatapp").build()

window.initializeFlowbite = (dotnet) => {
    dotNet = dotnet
    initFlowbite()
}

window.autoScrollDownChatbox = (force=false) => {
    const chatbox = document.getElementById('chatbox')
    function isScrollbarAtBottom(element){
        const offset = Math.abs(element.scrollHeight - element.clientHeight) * .40
        return Math.abs(element.scrollHeight - element.clientHeight - element.scrollTop) <= offset
    }
    
    if (chatbox) {
        if (force) {
            setTimeout(() => {
                chatbox.style.scrollBehavior = "smooth"
                chatbox.scrollTop = chatbox.scrollHeight
            }, 500)
        }
        else {
            if (isScrollbarAtBottom(chatbox)) {
                setTimeout(() => {
                    chatbox.style.scrollBehavior = "smooth"
                    chatbox.scrollTop = chatbox.scrollHeight
                }, 500)
            }
        }
    }
}

window.getUserAgentPlatformType = () => {
    const browser = Bowser.getParser(window.navigator.userAgent);
    return browser.getPlatform().type
}

window.onSidebarUserClick = async () => {
    const sidebarBtn = document.getElementById("default-sidebar-btn")
    if (getUserAgentPlatformType() === "mobile") {
        sidebarBtn.click()
    }

    autoScrollDownChatbox(true)
}  

window.disconnectedUsers = (id) => {
    const element = document.getElementById(id + "-online-status")
    if (element) {
        element.classList.remove("bg-green-400")
        element.classList.add("bg-red-400")
    }
}

window.receivedMessage = async (senderId, receiverId, content) => { 
    await dotNet.invokeMethodAsync("UpdateMessages")
    await dotNet.invokeMethodAsync("UpdateLastestMessages")
    autoScrollDownChatbox()
}

window.sendMessage = async (senderId, receiverId, content) => {
    await connection.send("SendMessage", senderId, receiverId, content) 
}

window.initHubConnection = async (userId)  => {
    connection = new signalR.HubConnectionBuilder().withUrl(`/_chatapp?userId=${userId}`).build() 
     
    connection.on("DisconnectedUser", disconnectedUsers)
    connection.on("ReceivedMessage", receivedMessage)
    
    await connection.start() 
    
    if (connection.connectionId) {
        console.log("Connected") 
    } 
}
