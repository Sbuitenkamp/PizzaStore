let socket;
let forms = [];
const order = {
    OrderCustomer: {},
    Pizzas: [],
    TimeOfOrder: null
}

window.addEventListener("load", () => {
    forms = document.querySelectorAll("form");
});

function openConnection() {
    socket = new WebSocket('ws://localhost:5050/ws');

    socket.onopen = function(e) {
        // disable the regular HTTP functions
        forms.forEach(form => {
            form.removeAttribute("method");
            form.removeAttribute("action");
            form.setAttribute("onsubmit", "return false");
        });
        document.querySelector("input#CustomerSubmit").setAttribute("onclick", "createOrder(this.parentNode)");
        document.querySelector("button#websocketButton").remove();
        console.log("[open] Connection established");
    };

    socket.onmessage = function(event) {
        const msg = JSON.parse(event.data);
        console.log(`[message] Data received from server: `);
        // console.log(msg);
        if (msg.Pizzas.length > 0) {
            order.Pizzas = msg.Pizzas;
            reRenderOrder();
        } else if (msg.OrderCustomer) {
            order.OrderCustomer = msg.OrderCustomer;
            order.TimeOfOrder = new Date(msg.TimeOfOrder).toLocaleDateString('nl-NL', { weekday:"long", year:"numeric", month:"short", day:"numeric"});
            renderPizzaForm();
        }
    };

    socket.onclose = function(event) {
        if (event.wasClean) {
            console.log(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`);
        } else {
            console.log('[close] Connection died');
        }
    };

    socket.onerror = function(error) {
        console.log(error);
    };
}

function sendMessage(msg) {
    console.log("[send] Sending to server");
    // console.log(msg);
    socket.send(JSON.stringify(msg));
}

function renderPizzaForm() {
    let pizzaOptionsHtml = "";
    let toppingsHtml = "";
    
    const leftContainer = document.querySelector("div.left");
    
    for (const pizzaName in pizzaNamesEnum) pizzaOptionsHtml += `<option value="${pizzaName}">${pizzaName}</option>`;
    for (const topping in toppingsEnum) {
        toppingsHtml += `
            <input name="${topping}.Add" type="checkbox" value="${topping}"/>${topping}
            <input name="${topping}.Amount" type="number" value="1"/>
            <br>
        `;
    }
    
    const leftHtml = `
        <form id="AddPizza" onsubmit="return false">
            <label for="PizzaName">Selecteer een pizza:
                <select name="PizzaName">
                    ${pizzaOptionsHtml}
                </select>
            </label>
            <br>
            ${toppingsHtml}
            <br>
            <label for="Amount">Aantal pizzas:
                <input type="number" name="amount" id="Amount" value="1">
            </label>
            <input type="submit" value="Toevoegen" onclick="addToOrder()">
        </form>
    `;
    
    const rightHtml = `
        <div class="right" style="float: right; width:  50%;">
            <h1>Bestellingoverzicht</h1>
            <p>Bezorgen bij: ${order.OrderCustomer.Name} ${order.OrderCustomer.Street} ${order.OrderCustomer.HouseNumber} ${order.OrderCustomer.ZipCode} ${order.OrderCustomer.City}</p>
            <p>Besteld op: ${order.TimeOfOrder}</p>
        </div>
    `;

    document.querySelector("form#Customer").remove();
    leftContainer.innerHTML += leftHtml;
    document.querySelector("body").innerHTML += rightHtml;
}

function reRenderOrder() {
    const rightContainer = document.querySelector("div.right");
    let rightHtml = `
        <h1>Bestellingoverzicht</h1>
        <p>Bezorgen bij: ${order.OrderCustomer.Name} ${order.OrderCustomer.Street} ${order.OrderCustomer.HouseNumber} ${order.OrderCustomer.ZipCode} ${order.OrderCustomer.City}</p>
    `;
    
    for (const pizza of order.Pizzas) {
        let pizzaHtml = `${pizza.Amount} ${pizza.PizzaType.Name}<br>Toppings:<br>`;
        for (const ingredient of pizza.PizzaType.Ingredients) {
            if (ingredient.Name === "Bodem") continue;
            pizzaHtml += `${ingredient.Name} x${ingredient.Amount}<br>`;
        }
        rightHtml += pizzaHtml + "<br>";
    }
    
    rightHtml += `<p>Besteld op: ${order.TimeOfOrder}</p>`;
    rightContainer.innerHTML = rightHtml;
}

function createOrder(form) {
    sendMessage(parseForm(form));
}

function addToOrder() {
    const pizzaForm = document.querySelector("form#AddPizza");
    const rightContainer = document.querySelector("div.right");
    const result = parseForm(pizzaForm);
    sendMessage(result);
    // `<p>@pizzaAmount.Amount @pizzaAmount.PizzaType.ToString()</p>`
}

function parseForm(form) {
    const result = { };
    const children = [...form.children]
        .map(x => {
            if (x.tagName === "INPUT" || x.tagName === "SELECT") return x;
            if (x.tagName === "LABEL") {
                const input= x.querySelector("input"); // get inputs that are embedded inside labels
                if (!input) return x.querySelector("select");
                return input;
            }
        })
        .filter(n => n); // filter for undefined

    if (form.id === "Customer") {
        result.OrderCustomer = {};
        for (const input of children) {
            if (["hidden", "submit"].includes(input.type) || input.name === "_RequestVerificationToken") continue;
            result.OrderCustomer[input.name] = input.type === "number" ? Number(input.value) : input.checkbox ? input.checked : input.value;
        }
    }
    
    if (form.id === "AddPizza") {
        result.Pizzas = [];
        let Amount = 1;
        const pizza = {
            Ingredients: []
        };
        
        // checkboxes first
        children.sort((a,b) => {
            if (a.type === "checkbox") return -1;
            if (b.type === "checkbox") return 1;
            return 0;
        });
        
        for (const input of children) {
            if (["submit"].includes(input.type) || input.name === "_RequestVerificationToken") continue;
            if (input.tagName === "SELECT") pizza.Name = input.value;
            else if (input.name === "amount") Amount = Number(input.value);
            else {
                if (input.checked) pizza.Ingredients[pizza.Ingredients.length] = { 
                    Name: input.value,
                    Amount: Number(children.find(x => x.name === `${input.value}.Amount`).value)
                }
            }
        }
        
        result.Pizzas[result.Pizzas.length] = { 
            Amount,
            PizzaType: pizza
        };
    }
    
    return result;
}