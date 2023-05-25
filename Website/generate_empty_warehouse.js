async function addContainers() {
  for (let s = 1; s <= 5; s++) {
    for (let c = 1; c <= 5; c++) {
      for (let r = 1; r <= 4; r++) {
        const address = "https://localhost:7032/api/Containers/" + r + "/" + c + "/" + s;
        // send POST request
        console.log("Adding container:", r, c, s);

        try {
          const response = await fetch(address, {
            mode: "no-cors",
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
          });

          console.log('response.status: ', response.status);
          console.log(response);

          if (!response.ok) {
            console.log('Response not ok');
            document.getElementById("errorMessage").innerHTML = "ERROR: " + response.status;
          }
        } catch (error) {
          console.log(error);
        }
      }
    }
  }
}