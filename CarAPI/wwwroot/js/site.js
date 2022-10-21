const uriCar = 'api/car';
const uriTelemetry = 'api/telemetry';
let cars = [];
let telemetries = [];


function getItems() {
    getCars();
    getTelemetries();
}

function getCars() {
    fetch(uriCar)
        .then(response => response.json())
        .then(data => _displayCars(data))
        .catch(error => console.error('Unable to get items.', error));
}

function getTelemetries() {
    fetch(uriTelemetry)
        .then(response => response.json())
        .then(data => _displayTelemetries(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addCar() {
    const carNameTextbox = document.getElementById('car-name');
    const carTypeTextbox = document.getElementById('car-type');
    const carDateCreated = document.getElementById('car-date-created');
    const carDateModified = document.getElementById('car-date-modified');

    const item = {
        name: carNameTextbox.value.trim(),
        type: carTypeTextbox.value.trim(),
    };

    fetch(uriCar, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getCars();
            carNameTextbox.value = '';
            carTypeTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function addTelemetry() {
    const carIdDropDown = document.getElementById('telemetry-carId-dropdown');
    const latitudeBox = document.getElementById('telemetry-latitude');
    const longitudeBox = document.getElementById('telemetry-longitude');
    const capacityBox = document.getElementById('telemetry-capacity');
    const speedBox = document.getElementById('telemetry-speed');

    console.log(carIdDropDown.value);

    const item = {
        carId: carIdDropDown.value,
        latitude: latitudeBox.value.trim(),
        longitude: longitudeBox.value.trim(),
        capacity: capacityBox.value.trim(),
        speed: speedBox.value.trim(),
    };
    fetch(uriTelemetry, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getTelemetries();
            latitudeBox.value = '';
            longitudeBox.value = '';
            capacityBox.value = '';
            speedBox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteCar(id) {
    deleteItem(uriCar, id);
}

function deleteTelemetry(id) {
    deleteItem(uriTelemetry, id);
}

function deleteItem(uri, id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayCarEditForm(id) {
    const item = cars.find(item => item.idCar === id);
    document.getElementById('edit-car-id').value = item.idCar;
    document.getElementById('edit-car-name').value = item.name;
    document.getElementById('edit-car-type').value = item.type;
    document.getElementById('editCarForm').style.display = 'block';
}

function displayTelemetryEditForm(id) {
    const item = telemetries.find(item => item.idTelemetry === id);
    document.getElementById('edit-telemetry-id').value = item.idTelemetry;
    document.getElementById('edit-telemetry-idCar').value = item.idCar;
    document.getElementById('edit-telemetry-longitude').value = item.longitude;
    document.getElementById('edit-telemetry-latitude').value = item.latitude;
    document.getElementById('edit-telemetry-speed').value = item.speed;
    document.getElementById('edit-telemetry-capacity').value = item.capacity;
    document.getElementById('editTelemetryForm').style.display = 'block';
}

function updateCar() {
    const id = document.getElementById('edit-car-id').value;
    const item = {
        idCar: id,
        name: document.getElementById('edit-car-name').value.trim(),
        type: document.getElementById('edit-car-type').value.trim()
    };

    fetch(`${uriCar}/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getCars())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function updateTelemetry() {
    const id = document.getElementById('edit-telemetry-id').value;
    const item = {
        idTelemetry: id,
        idCar: document.getElementById('edit-telemetry-idCar').value,
        latitude: document.getElementById('edit-telemetry-latitude').value.trim(),
        longitude: document.getElementById('edit-telemetry-longitude').value.trim(),
        speed: document.getElementById('edit-telemetry-speed').value.trim(),
        capacity: document.getElementById('edit-telemetry-capacity').value.trim(),
    };

    fetch(`${uriTelemetry}/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getTelemetries())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editCarForm').style.display = 'none';
    document.getElementById('editTelemetryForm').style.display = 'none';
}

function _displayCount(type, itemCount) {
    let name = '';
    if (type === 'telemetry') {
        name = (itemCount === 1) ? 'telemetry' : 'telemetries';
    } else if (type === 'car') {
        name = (itemCount === 1) ? 'car' : 'cars';
    }
    document.getElementById(`${type}-counter`).innerText = `${itemCount} ${name}`;
}

function _displayCars(data) {
    const tBody = document.getElementById('cars');
    tBody.innerHTML = '';

    const carDropdown = document.getElementById('telemetry-carId-dropdown');
    carDropdown.innerHTML = '';

    _displayCount('car', data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayCarEditForm(${item.idCar})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteCar(${item.idCar})`);

        let tr = tBody.insertRow();

        let i = 0;
        Object.keys(item).forEach(key => {
            console.log(key, item[key]);
            let td = tr.insertCell(i);
            let nameNode = document.createTextNode(item[key]);
            td.appendChild(nameNode);
            i++;
        });

        let tdEdit = tr.insertCell(5);
        tdEdit.appendChild(editButton);

        let tdDelete = tr.insertCell(6);
        tdDelete.appendChild(deleteButton);

        let option = document.createElement("option");
        option.setAttribute('value', item.idCar);

        let optionText = document.createTextNode(item.idCar);
        option.appendChild(optionText);

        carDropdown.appendChild(option);
    });

    cars = data;
}

function _displayTelemetries(data) {
    const tBody = document.getElementById('telemetries');
    tBody.innerHTML = '';

    _displayCount('telemetry', data.length);

    const button = document.createElement('button');

    data.forEach(item => {

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayTelemetryEditForm(${item.idTelemetry})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteTelemetry(${item.idTelemetry})`);

        let tr = tBody.insertRow();

        let i = 0;
        Object.keys(item).forEach(key => {
            console.log(key, item[key]);
            let td = tr.insertCell(i);
            let nameNode = document.createTextNode(item[key]);
            td.appendChild(nameNode);
            i++;
        });

        let tdEdit = tr.insertCell(6);
        tdEdit.appendChild(editButton);

        let tdDelete = tr.insertCell(7);
        tdDelete.appendChild(deleteButton);
    });

    telemetries = data;

}