import "flowbite"  
import { initFlowbite } from "flowbite"
import Bowser from "bowser"

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

