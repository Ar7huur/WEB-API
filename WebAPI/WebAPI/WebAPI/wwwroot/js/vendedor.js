$(document).ready(function () {
    carregarDados();
});

$(".btnsalvar").click(function () {
    var vendedor = {
        VendedorID: $('.vendedorID').val(),
        VendedorNome: $('#nomevendedor').val(),
        VendedorEmail: $('#email').val(),
        VendedorSalario: parseFloat($('#salario').val())
        
    };
    if (valForm(vendedor.VendedorNome, vendedor.VendedorEmail, parseFloat(vendedor.VendedorSalario))) {
        console.log(vendedor.VendedorID);
        if (parseInt(vendedor.VendedorID) > 0) {
            atualizaVendedor(vendedor);
        } else {
            criaVendedor(vendedor);
        }
    }
    console.log(departamento);
});



function carregarDados() {
    $.ajax({
        url: "/Vendedores/pegarVendedores",
        method: "GET",
        success: function (vendedores) {
            montarTabela(vendedores);
        }
    });
    function montarTabela(vendedores) {
        var indice = 0;
        var tabelaDiv = document.getElementById("tabelaDiv");
        var tabela = '<table class="table table-striped">';
        tabela += '<thead>';
        tabela += '<tr>';
        tabela += '<th scope="col">ID</th>';
        tabela += '<th scope="col">Nome</th>';
        tabela += '<th scope="col">Email</th>';
        tabela += '<th scope="col">Salario</th>';
        //tabela += '<th scope="col">Departamento</th>';
        tabela += '<th scope="col">Ferramentas</th>';
        tabela += '</tr>';
        tabela += '</thead>';
        tabela += '<tbody>';
        for (indice = 0; indice < vendedores.length; indice++) {
            //console.log(departamentos[indice].id);
            tabela += `<tr id="${vendedores[indice].vendedorID}">
                  <td>${vendedores[indice].vendedorID}</td>
                  <td>${vendedores[indice].vendedorNome}</td>
                  <td>${vendedores[indice].vendedorEmail}</td>
                  <td>${vendedores[indice].vendedorSalario}</td>
                      <td><button type="button" class="btn btn-outline-warning" onclick='pegarVenPeloId(${vendedores[indice].vendedorID})'>Atualizar</button> 
                        <button type="button" class="btn btn-outline-danger" onclick='DeletarVenPeloId(${vendedores[indice].vendedorID})'>Deletar</button></td>
                        </tr >`;

        }
        tabela += '</tbody>';
        tabela += '</table>';
        tabelaDiv.innerHTML = tabela;

    }
}
$("#novoVendedor").click(function () {
    TitModal("Cadastro simples de vendedores");
    MostraModal();
    limpaModal();//limpa a modal
    $('#vendedorID').val(0);
});

function TitModal(texto) {
    $(".modal-Title").text(texto);
}

function MostraModal() {
    new bootstrap.Modal($("#Modal"), {}).show();
}

function limpaModal() {
    $(".nomevendedor").val('');
    $(".nomevendedor").removeClass('is-valid');

    $(".email").val('');
    $(".email").removeClass('is-valid');

    $(".salario").val('');
    $(".salario").removeClass('is-valid');

    $(".departamento").val('');
    $(".departamento").removeClass('is-valid');
}

function criaVendedor(vendedor) {
    
    $.ajax({
        url: "/Vendedores/novoVendedor",
        method: "POST",
        data: {
            vendedor: vendedor
        },
        success: function (result) {
            $("#Modal").modal('hide');
            carregarDados();
        }

    });
}



function atualizaVendedor(vendedor) {
    $.ajax({
        url: "/Vendedores/atualizarVendedores",
        method: "POST",
        data: {
            vendedor: vendedor
        },
        success: function (vendedorAtualizado) {
            $("#Modal").modal('hide');
            var linhaTabela = $(`#${vendedorAtualizado.vendedorID}`);
            carregarDados();
            limpaModal();
        }


    });
}

function pegarVenPeloId(vendedorId) {
    $.ajax({
        url: "/Vendedores/pegarVendedorID",
        method: "GET",
        data: {
            vendedorId: vendedorId
        },
        success: function (result) {
            console.log(vendedorId);
            MostraModal(result);
            TitModal(`Atualizar o vendedor ${result.vendedorNome}`);
            $(".vendedorID").val(result.vendedorID);
            $(".nomevendedor").val(result.vendedorNome);
            $(".email").val(result.vendedorEmail);
            $(".salario").val(result.vendedorSalario);

        }
    });
}


function DeletarVenPeloId(vendedorId) {
    $.ajax({
        url: "/Vendedores/excluirVendedores",
        method: "POST",
        data: {
            vendedorId: vendedorId
        },
        success: function (status) {
            if (status) {
                $('.close').alert();
                document.getElementById(vendedorId).remove();
            }
            else {
                $('.close').alert();
            }
        }

    });

}

function valForm(VendedorNome, VendedorEmail,VendedorSalario) {
    let NomeValido = valNome(VendedorNome);
    let EmailValido = valEmail(VendedorEmail);
    let SalarioValido = valSal(VendedorSalario);
    if (NomeValido == true && EmailValido == true && SalarioValido ==true ) {
        return true;
    }
    return false;
}

function valNome(VendedorNome) {
    console.log(VendedorNome);
    if (VendedorNome.trim() == '' || VendedorNome == undefined || VendedorNome.length < 10) {
        $(".VendedorNome").addClass('is-invalid');
        $(".erroNomevendedor").text("Preencha corretamente o nome.");
        $(".erroNomevendedor").removeClass("d-none");
        return false;
    } else if (VendedorNome.length > 30) {
        $(".VendedorNome").addClass('is-invalid');
        $(".erroNomevendedor").text("Excedeu o limite de 30 caracteres");
        $(".erroNomevendedor").removeClass("d-none");
        return false
    } else {
        $(".VendedorNome").removeClass('is-invalid');
        $(".VendedorNome").addClass('is-valid');
        $(".erroNomevendedor").addClass("d-none");
        return true;
    }
}

function valEmail(VendedorEmail) {
    if (VendedorEmail.trim() == '' || VendedorEmail == undefined || VendedorEmail.length < 10) {
        $(".VendedorEmail").addClass('is-invalid');
        $(".erroEmail").text("Preencha corretamente email.");
        $(".erroEmail").removeClass("d-none");
        return false;
    } else if (VendedorEmail.length > 35) {
        $(".VendedorEmail").addClass('is-invalid');
        $(".erroEmail").text("Excedeu o limite de 30 caracteres por email");
        $(".erroEmail").removeClass("d-none");
        return false
    } else {
        $(".VendedorEmail").removeClass('is-invalid');
        $(".VendedorEmail").addClass('is-valid');
        $(".erroEmail").addClass("d-none");
        return true;
    }
}

function valSal(VendedorSalario) {
    if (VendedorSalario == undefined || VendedorSalario < 1) {
        $(".VendedorSalario").addClass('is-invalid');
        $(".erroSalario").text("Preencha corretamente email.");
        $(".erroSalario").removeClass("d-none");
        return false;
    } else if (VendedorSalario.length > 4) {
        $(".VendedorSalario").addClass('is-invalid');
        $(".erroSalario").text("Excedeu a cota salarial.");
        $(".erroSalario").removeClass("d-none");
        return false;
    } else {
        $(".VendedorSalario").removeClass('is-invalid');
        $(".VendedorSalario").addClass('is-valid');
        $(".erroSalario").addClass("d-none");
        return true;
    }

}