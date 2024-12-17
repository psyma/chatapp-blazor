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
async function initHubConnection() {
    let connection = new signalR.HubConnectionBuilder().withUrl("/_chatapp?userId=myid").build() 
    await connection.start() 
    
    if (connection.connectionId) {
        console.log("Connected")
    
    }
    await connection.send("Send", "sender", "receiver", "message")
}

await initHubConnection()
