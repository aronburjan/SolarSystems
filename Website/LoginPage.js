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
			response.json().then(data => {
				sessionStorage.setItem('isLoggedIn', true);
				sessionStorage.setItem('accountType', data[0].accessLevel);
				switch(data[0].accessLevel){
					case 0: 
						window.open(
						"ComponentManager.html","_self")
					break;
					case 1: 
						window.open(
						"ProjectManager.html","_self")
					break;
					case 2: 
						window.open(
						"WarehouseWorker.html","_self")
					break;
					default:
						console.log('Unknown account type');
						document.getElementById("errorMessage").innerHTML = "ERROR: Incorrect Account Type";
					break;
				}
			});
		}
    }) 
    .catch((err) => {
      console.log(err);
    });	


	
}