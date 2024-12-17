import "flowbite"  
import Bowser from "bowser" 
import { initFlowbite } from "flowbite"
import * as signalR from '@microsoft/signalr'

window.initializeFlowbite = () => {
    initFlowbite();
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

window.initHubConnection = async (userId)  => {
    let connection = new signalR.HubConnectionBuilder().withUrl(`/_chatapp?userId=${userId}`).build() 
    
    connection.on("ConnectedUsers", connectedUsers)
    connection.on("DisconnectedUser", disconnectedUsers)
    
    await connection.start() 
    
    if (connection.connectionId) {
        console.log("Connected") 
    } 
}
