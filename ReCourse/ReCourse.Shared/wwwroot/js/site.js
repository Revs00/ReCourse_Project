// site.js

window.appHelpers = {
    confirmDelete: function (message) {
        return confirm(message);
    },

    showToast: function (message, type) {
        const colors = {
            success: "#2ecc71",
            error: "#e74c3c",
            info: "#3498db",
            warning: "#f1c40f"
        };

        const toast = document.createElement("div");
        toast.innerText = message;
        toast.style.position = "fixed";
        toast.style.bottom = "20px";
        toast.style.right = "20px";
        toast.style.backgroundColor = colors[type] || "#333";
        toast.style.color = "white";
        toast.style.padding = "12px 18px";
        toast.style.borderRadius = "8px";
        toast.style.boxShadow = "0 4px 10px rgba(0,0,0,0.1)";
        toast.style.zIndex = "10000";
        toast.style.fontSize = "0.95rem";
        toast.style.opacity = "0";
        toast.style.transition = "opacity 0.3s ease";

        document.body.appendChild(toast);

        setTimeout(() => (toast.style.opacity = "1"), 10);
        setTimeout(() => {
            toast.style.opacity = "0";
            setTimeout(() => toast.remove(), 300);
        }, 3000);
    },

    fadeIn: function (selector) {
        const el = document.querySelector(selector);
        if (el) {
            el.style.opacity = 0;
            el.style.transition = "opacity 0.5s ease";
            requestAnimationFrame(() => {
                el.style.opacity = 1;
            });
        }
    }
};
