$(document).ready(function () {
    carregaDados();
});

function carregaDados() {
    $.ajax({
        url: "/Departamentos/pegarDepartamentos",
        method: "GET",
        success: function (departamentos) {
            montarTabela(departamentos);
        }
    });
    function montarTabela(departamentos) {
        var indice = 0;
        var divTabela = document.getElementById("divTabela");
        var tabela = '<table class="table table-striped">';
        tabela += '<thead>';
        tabela += '<tr>';
        tabela += '<th scope="col">ID</th>';
        tabela += '<th scope="col">Nome do departamento</th>';
        tabela += '<th scope="col">Ferramentas</th>';
        tabela += '</tr>';
        tabela += '</thead>';
        tabela += '<tbody>';
        for (indice = 0; indice < departamentos.length; indice++) {
            //console.log(departamentos[indice].id);
            tabela += `<tr id="${departamentos[indice].id}">
                        <td>${departamentos[indice].id}</td>
                        <td>${departamentos[indice].nome}</td>
                        <td><button type="button" class="btn btn-outline-warning" onclick='pegarDepPeloId(${departamentos[indice].id})'>Atualizar</button> 
                        <button type="button" class="btn btn-outline-danger" onclick='DeletarDepPeloId(${departamentos[indice].id})'>Deletar</button></td>
                        </tr >`;

        }
        tabela += '</tbody>';
        tabela += '</table>';
        divTabela.innerHTML = tabela;

    }
}
  

 //modal de cadastro:
$("#novoDepartamento").click(function () {
    TituloModal("Cadastro de novo departamento");
    MostrarModal();
    limparModal();//limpa a modal
    $('#depID').val(0);
});
function TituloModal(texto) {
    $(".modal-title").text(texto);
}
function MostrarModal() {
    new bootstrap.Modal($("#modal"), {}).show();
}
function limparModal() {
    $(".nome").val('');
    $(".nome").removeClass('is-valid');
}

function criarDepartamento(departamento) {
    $.ajax({
        url: "/Departamentos/novoDepartamento",
        method: "POST",
        data: {
            departamento: departamento
        },
        success: function (result) {
            $("#modal").modal('hide');
            carregaDados();
        }

    });
}
function pegarDepPeloId(departamentoId) {
    $.ajax({
        url: "/Departamentos/pegarDepartamentoPeloID",
        method: "GET",
        data: {
            departamentoId: departamentoId
        },
        success: function (result) {
            console.log(result);
            MostrarModal();

            TituloModal(`Atualizar categoria ${result.nome}`);

            $(".depID").val(result.id);
            $(".nome").val(result.nome);

        }
    })
}

function AttDepartamentos(departamento){
    $.ajax({
        url: "/Departamentos/atualizarDepartamento",
        method: "POST",
        data: {
            departamento: departamento
        },
        success: function(departamentoAtualizado) {
            $("#modal").modal('hide');
            var linhaTabela = $(`#${departamentoAtualizado.id}`);
            carregaDados();
            limparModal();
        }


    })
}

function DeletarDepPeloId(departamentoId) {
    $.ajax({
        url: "/Departamentos/deletarDepartamento",
        method: "POST",
        data: {
            departamentoId: departamentoId
        },
        success: function (status) {
            if (status) {
                $('.close').alert();
                document.getElementById(departamentoId).remove();
            }
            else {
                $('.close').alert();
            }
        }

    })

}
$(".btnSalvar").click(function () {
    var departamento = {
        ID: $('#depID').val(),
        nome: $('.nome').val()
    };

    if (validarForm(departamento)) {
        console.log(departamento);

        if (departamento.ID > 0) {
            AttDepartamentos(departamento);
        } else
            criarDepartamento(departamento);
    }
});


function validarForm(departamento) {
    let nomeValido = validarNome(departamento.nome);
    if (nomeValido == true) {
        return true;
    }
    return false;
}
function validarNome(nome) {
    if (nome.trim() == '' || nome == undefined) {
        $(".nome").addClass('is-invalid');
        $(".erroNome").text("Preencha corretamente o campo de nome de departamento para poder prosseguir.");
        $(".erroNome").removeClass("d-none");
        return false;
    } else if (nome.length > 30) {
        $(".nome").addClass('is-invalid');
        $(".erroNome").text("Execedeu o limite de 30 caracteres, use menos caracteres para prosseguir.");
        $(".erroNome").removeClass("d-none");
        return false;
    } else {
        $(".nome").removeClass('is-invalid');
        $(".nome").addClass('is-valid');
        $(".erroNome").addClass("d-none");
        return true;
    }

}

