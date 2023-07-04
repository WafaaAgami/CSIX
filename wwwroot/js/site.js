function DeleteTask(Id, Name) {

    const response = confirm("Are you sure you want to delete '" + Name + "'?");
    if (response === true) {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow'
        };
        fetch(`/Tasks/Delete/${Id}`, requestOptions)
            .then(response => response.json())
            .then(result => {
                try {
                    if (result.isSaved === true) {
                        alert('Deleted');
                    }
                    else {
                        debugger;
                        alert('Error Occured');
                    }
                } catch (er) {
                    alert('Error');
                }
            })
            .catch(error => console.log('error', error));

    }
}
function GetValueFromElementById(Id) {
    if (document.getElementById(Id) !== null) {
        return document.getElementById(Id).value;
    }
    return "";
}
function All_CheckSuccess(result) {
    try {
        if (result.isSaved === true) {
            alert('Save');
        }
        else {
            ErrorHandling(result);
            alert(result.message);
        }
    }
    catch (err) {
        ErrorHandling(err);
    }
}
function ResetValidations() {
    try {
        var _validations = document.getElementsByClassName("text-danger");
        var pos = 0;
        while (pos < _validations.length) {
            _validations[pos].innerHTML = '';
            pos++;
        }
    }
    catch (err) {
        ErrorHandling(err);
    }
}
function ErrorHandling(err) {
    console.log(err);
}
function Activity_Create() {
    try {
        ResetValidations();
        var _isok = true;

        var temp = {
            Id: GetValueFromElementById("ActivityId"),
            Name: GetValueFromElementById("ActivityName"),
            Description: GetValueFromElementById("ActivityDescription")
        };

        if (temp.Name === '') {
            _isok = false;
            document.getElementById("ActivityNameValidation").innerHTML = 'This is Mandatory';
        }
        if (_isok) {
            CloseModal("ActivityModal");
            var myHeaders = new Headers();
            myHeaders.append("Content-Type", "application/json");
            var raw = JSON.stringify(temp);
            var requestOptions = {
                method: 'POST',
                headers: myHeaders,
                body: raw,
                redirect: 'follow'
            };
            fetch("/Activities/Create", requestOptions)
                .then(response => response.json())
                .then(result => All_CheckSuccess(result))
                .catch(error => ErrorHandling(error));
        }
    }
    catch (error) {
        ErrorHandling(error);
    }
}
function OpenActivities(Id, Name, Details) {
    try {
        document.getElementById("ActivityId").value = Id;
        document.getElementById("ActivityName").value = Name;
        document.getElementById("ActivityDescription").value = Details;
    }
    catch (err) {
        ErrorHandling(err);
    }
    OpenModalItem("ActivityModal");
}


function MyTask_Create() {
    try {
        ResetValidations();
        var _isok = true;

        var temp = {
            Id: GetValueFromElementById("MyTaskId"),
            Name: GetValueFromElementById("MyTaskName"),
            Description: GetValueFromElementById("MyTaskDescription"),
            Status: GetValueFromElementById("MyTaskStatus"),
            UserId: GetValueFromElementById("MyTaskUser"),
            ActivityId: GetValueFromElementById("MyTaskActivity")
        };

        if (temp.Name === '') {
            _isok = false;
            document.getElementById("MyTaskNameValidation").innerHTML = 'This is Mandatory';
        }
        if (_isok) {
            CloseModal("MyTaskModal");
            var myHeaders = new Headers();
            myHeaders.append("Content-Type", "application/json");
            var raw = JSON.stringify(temp);
            var requestOptions = {
                method: 'POST',
                headers: myHeaders,
                body: raw,
                redirect: 'follow'
            };
            fetch("/Tasks/Create", requestOptions)
                .then(response => response.json())
                .then(result => All_CheckSuccess(result))
                .catch(error => ErrorHandling(error));
        }
    }
    catch (error) {
        ErrorHandling(error);
    }
}
function OpenMyTasks(Id, Name, Details, Status, ActivityId, UserId) {
    try {
        document.getElementById("MyTaskId").value = Id;
        document.getElementById("MyTaskName").value = Name;
        document.getElementById("MyTaskDescription").value = Details;
        document.getElementById("MyTaskStatus").value = Status;
        document.getElementById("MyTaskActivity").value = ActivityId;
        document.getElementById("MyTaskUser").value = UserId;

    }
    catch (err) {
        ErrorHandling(err);
    }
    OpenModalItem("MyTaskModal");
}


function Task_Create() {
    try {
        ResetValidations();
        var _isok = true;

        var temp = {
            Id: GetValueFromElementById("TaskId"),
            Name: GetValueFromElementById("TaskName"),
            Description: GetValueFromElementById("TaskDescription"),
            Status: GetValueFromElementById("TaskStatus"),
            UserId: GetValueFromElementById("TaskUser"),
            ActivityId: GetValueFromElementById("TaskActivity")
        };

        if (temp.Name === '') {
            _isok = false;
            document.getElementById("TaskNameValidation").innerHTML = 'This is Mandatory';
        }
        if (_isok) {
            CloseModal("TaskModal");
            var myHeaders = new Headers();
            myHeaders.append("Content-Type", "application/json");
            var raw = JSON.stringify(temp);
            var requestOptions = {
                method: 'POST',
                headers: myHeaders,
                body: raw,
                redirect: 'follow'
            };
            fetch("/Tasks/Create", requestOptions)
                .then(response => response.json())
                .then(result => All_CheckSuccess(result))
                .catch(error => ErrorHandling(error));
        }
    }
    catch (error) {
        ErrorHandling(error);
    }
}
function OpenTasks(Id, Name, Details, Status, ActivityId, UserId) {
    try {
        document.getElementById("TaskId").value = Id;
        document.getElementById("TaskName").value = Name;
        document.getElementById("TaskDescription").value = Details;
        document.getElementById("TaskStatus").value = Status;
        document.getElementById("TaskActivity").value = ActivityId;
        document.getElementById("TaskUser").value = UserId;

    }
    catch (err) {
        ErrorHandling(err);
    }
    OpenModalItem("TaskModal");
}



function OpenModalItem(modalId) {
    var modal = document.getElementById(modalId);
    modal.className = "modal fade show";
    modal.style.display = "block";
}
function CloseModal(modalId) {
    var modal = document.getElementById(modalId);
    modal.className = "modal fade";
    modal.style.display = "none";
}


const connection = new signalR.HubConnectionBuilder()
    .withUrl("/CheckTasks")
    .build();

connection.on("ReceiveMessage", (user, message) => {

    console.log(`Received message from ${user}: ${message}`);
});


connection.on("AllTasksDeleted", (user, message) => {
    var table = document.getElementById("AllTasksBody");
    var el = document.getElementById(`AllTasks_${message}`);

    if (table != null && el != null) {
        table.deleteRow(el.rowIndex - 1);
    }
});

connection.on("MyTaskDeleted", (user, message) => {
    var el = document.getElementById(`MyTasks_${message}`);
    var table = document.getElementById("MyTasksBody");
    if (table != null && el != null) {
        table.deleteRow(el.rowIndex - 1);
    }
});


connection.on("MyTaskUpdate", (user, message) => {
    try {
        var el = document.getElementById(`MyTasks_${message[0].id}`);
        if (el === null) {
            var table = document.getElementById("MyTasksBody");

            var row = table.insertRow(0);
            row.id = `MyTasks_${message[0].id}`;
            row.className = "Item UpdatedRow";

            var cell_Name = row.insertCell(0);
            var cell_Description = row.insertCell(1);
            var cell_Status = row.insertCell(2);
            var cell_Activity = row.insertCell(3);
            var cell_Actions = row.insertCell(4);
        }
        else {
            try {
                var cell_Name = el.cells[0];
                var cell_Description = el.cells[1];
                var cell_Status = el.cells[2];
                var cell_Activity = el.cells[3];
                var cell_Actions = el.cells[4];
                while (cell_Actions.firstChild) {
                    cell_Actions.removeChild(cell_Actions.firstChild);
                }
            }
            catch (eeee) {
                console.log(eeee);
            }
        }

        cell_Name.textContent = message[0].name;
        cell_Description.textContent = message[0].description;
        cell_Status.textContent = message[0].statusName;
        cell_Activity.textContent = message[0].activityName;
        var EditAction = document.createElement("span");
        EditAction.className = "Clickable";
        EditAction.onclick = function () {
            console.log('Here');
            OpenMyTasks(`${message[0].id}`, `${message[0].name}`, `${message[0].description}`, message[0].status, `${message[0].activityId}`, `${MyUserId}`)
        };
        var EditActionIcon = document.createElement("i");
        EditActionIcon.className = "fa fa-edit Black";
        EditAction.appendChild(EditActionIcon);
        cell_Actions.appendChild(EditAction);

        var DeleteAction = document.createElement("i");
        DeleteAction.className = "fa fa-close Black Clickable";
        DeleteAction.onclick = function () {
            DeleteTask(`${message[0].id}`, `${message[0].name}`);
            
        };
        cell_Actions.appendChild(DeleteAction);


    }
    catch (err) {
        console.log(err);
    }
});

connection.on("TaskUpdate", (user, message) => {
    var table = document.getElementById("AllTasksBody");
    if (table != null) {
        var el = document.getElementById(`AllTasks_${message[0].id}`);
        if (el === null) {

            var row = table.insertRow(0);
            row.id = `AllTasks_${message[0].id}`;
            row.className = "Item UpdatedRow";

            var cell_Name = row.insertCell(0);
            var cell_Description = row.insertCell(1);
            var cell_Status = row.insertCell(2);
            var cell_Activity = row.insertCell(3);
            var cell_User = row.insertCell(4);
            var cell_Actions = row.insertCell(5);
        }
        else {
            try {
                var cell_Name = el.cells[0];
                var cell_Description = el.cells[1];
                var cell_Status = el.cells[2];
                var cell_Activity = el.cells[3];
                var cell_User = el.cells[4];
                var cell_Actions = el.cells[5];
                while (cell_Actions.firstChild) {
                    cell_Actions.removeChild(cell_Actions.firstChild);
                }
            }
            catch (eeee) {
                console.log(eeee);
            }
        }

        cell_Name.textContent = message[0].name;
        cell_Description.textContent = message[0].description;
        cell_Status.textContent = message[0].statusName;
        cell_Activity.textContent = message[0].activityName;
        cell_User.textContent = message[0].userName;
        var EditAction = document.createElement("span");
        EditAction.className = "Clickable";
        EditAction.onclick = function () {
            console.log('Here');
            OpenTasks(`${message[0].id}`, `${message[0].name}`, `${message[0].description}`, message[0].status, `${message[0].activityId}`, `${message[0].userId}`)
        };
        var EditActionIcon = document.createElement("i");
        EditActionIcon.className = "fa fa-edit Black";
        EditAction.appendChild(EditActionIcon);
        cell_Actions.appendChild(EditAction);

        var DeleteAction = document.createElement("i");
        DeleteAction.className = "fa fa-close Black Clickable";
        DeleteAction.onclick = function () {
            DeleteTask(`${message[0].id}`, `${message[0].name}`);

        };
        cell_Actions.appendChild(DeleteAction);
    }
});

connection.start()
    .then(() => {
        console.log("Connected to SignalR hub.");
    })
    .catch(err => {
        console.error("Error connecting");
    })


