function validateLogin(){
	
	//get values from html page
	const form = {
		username: document.getElementById("usernameInput"),
		password: document.getElementById("passwordInput"),
		submit: document.querySelector("loginButton"),
	};
	
	
	console.log(JSON.stringify({
		username: form.username.value,
		password: form.password.value}))
	
	
	
	//address where requests are sent
	const loginServer = "https://localhost:7032/";
	
	//send request
	fetch(loginServer, {
		method: "POST",
		body: JSON.stringify({
			email: form.username.value,
			password: form.password.value,
		}),
	})
    .then(response => {
		console.log('response.status: ', response.status);
		console.log(response);
		if (!response.ok) {
			console.log('Response not ok');
			//todo
		} else {
		window.open(
			//redirect to next site
          "target.html")
		}
    }) 
    .catch((err) => {
      console.log(err);
    });	
	
	
}