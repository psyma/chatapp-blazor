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

window.getUserAgentPlatformType = () => {
    const browser = Bowser.getParser(window.navigator.userAgent);
    return browser.getPlatform().type
}

window.onSidebarUserClick = async () => {
    const sidebarBtn = document.getElementById("default-sidebar-btn") 
    if (getUserAgentPlatformType() === "mobile") {
        sidebarBtn.click()
    }
}

window.connectedUsers = (ids) => {
    for (const id of ids) {
        const element = document.getElementById(id + "-online-status")
        if (element) {
            element.classList.remove("bg-red-400")
            element.classList.add("bg-green-400")
        }
    }
}

window.disconnectedUsers = (id) => {
    const element = document.getElementById(id + "-online-status")
    if (element) {
        element.classList.remove("bg-green-400")
        element.classList.add("bg-red-400")
    }
}

window.receivedMessage = (senderId, receiverId, content) => { 
    dotNet.invokeMethodAsync("UpdateMessages")
    dotNet.invokeMethodAsync("UpdateLastMessages")
}

window.sendMessage = async (senderId, receiverId, content) => {
    await connection.send("SendMessage", senderId, receiverId, content) 
}

window.initHubConnection = async (userId)  => {
    connection = new signalR.HubConnectionBuilder().withUrl(`/_chatapp?userId=${userId}`).build() 
    
    connection.on("ConnectedUsers", connectedUsers)
    connection.on("DisconnectedUser", disconnectedUsers)
    connection.on("ReceivedMessage", receivedMessage)
    
    await connection.start() 
    
    if (connection.connectionId) {
        console.log("Connected") 
    } 
}
