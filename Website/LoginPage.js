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
	const loginServer = "https://localhost:7032/api/Users/" + form.username.value + "/" + form.password.value;
	
	//send request
	fetch(loginServer, {
		method: "POST",
		/*body: JSON.stringify({
			email: form.username.value,
			password: form.password.value,
		}),
		*/
	})
    .then(response => {
		console.log('response.status: ', response.status);
		console.log(response);
		if (!response.ok) {
			console.log('Response not ok');
			document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
		} else {
		sessionStorage.setItem('isLoggedIn', true);
		window.open(
			//redirect to next site
          "ComponentManager.html","_self")
		}
    }) 
    .catch((err) => {
      console.log(err);
    });	


	
}