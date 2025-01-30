document.getElementById("loginForm").addEventListener("submit", function (e) {
    e.preventDefault(); // Sayfa yenilemeyi engelle

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    if (email && password) {
        alert(`Welcome, ${email}!`);
    } else {
        alert("Please fill out all fields.");
    }
});
