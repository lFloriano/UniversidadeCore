// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SelecionarAlunosNaoMatriculados() {
    var listaEstudantes = Array.from(document.querySelector("#lgpEstudantes")
        .getElementsByClassName("list-group-item"))
        .filter((x) => !x.classList.contains("active"));

    return listaEstudantes
}

function ToggleListaAlunosMatriculados(mostrarApenasMatriculados) {
    var listaEstudantes = SelecionarAlunosNaoMatriculados();
    var hdfEstudantesSelecionados = document.querySelector("#idsEstudantesMatriculados");

    listaEstudantes.forEach((obj) => {
        if (mostrarApenasMatriculados == true) {
            $(obj).hide();
        }
        else {
            $(obj).show();
        }

    })
}

function AddMatriculasNoInput(objEstudante) {
    var inputAlvo = document.querySelector("#idsEstudantesMatriculados");
    var listaEstudantes = Array.from(document.querySelector("#lgpEstudantes").getElementsByClassName("list-group-item"));
    inputAlvo.value = "";

    if (objEstudante.classList.contains("active")) {
        objEstudante.classList.remove("active");
    }
    else {
        objEstudante.classList.add("active");
    }

    listaEstudantes.filter((item) => item.classList.contains("active"))
        .forEach((item) => inputAlvo.value += (item.getAttribute("data-valor") + ";"));

    ToggleListaAlunosMatriculados(document.querySelector("#ckbMostrarApenasSelecionados").checked);
}