$(document).ready(function () {
    carregaDado();
});
$(".botnSalvar").click(function () {
    var venda = {
        VendaID: $('.vendasID').val(),
        VendaData: $('#vendadt').val(),
        VendaValor: parseFloat($('#venval').val())

    };
    if (valForm(venda.VendaData, parseFloat(venda.VendaValor))) {
        console.log(venda.VendaID);
        if (parseInt(venda.VendaID) > 0) {
            atualizaVendas(venda);
        } else {
            criaVendas(venda);
        }
    }
    console.log(venda);
});
function carregaDado() {
    $.ajax({
        url: "/Vendas/pegarVendas",
        method: "GET",
        success: function (venda) {
            montarTab(venda);
        }
    });
    function montarTab(venda) {
        var indice = 0;
        var divTab = document.getElementById("divTab");
        var tabela = '<table class="table table-striped">';
        tabela += '<thead>';
        tabela += '<tr>';
        tabela += '<th scope="col">ID</th>';
        tabela += '<th scope="col">Data da venda</th>';
        tabela += '<th scope="col">Valor recorde da venda</th>';
        tabela += '<th scope="col">Ferramentas</th>';
        tabela += '</tr>';
        tabela += '</thead>';
        tabela += '<tbody>';
        for (indice = 0; indice < venda.length; indice++) {
            //console.log(departamentos[indice].id);
            tabela += `<tr id="${venda[indice].vendaID}">
                        <td>${venda[indice].vendaID}</td>
                        <td>${venda[indice].vendaData}</td>
                        <td>${venda[indice].vendaValor}</td>
                        <td><button type="button" class="btn btn-outline-warning" onclick='pegarVendasId(${venda[indice].vendaID})'>Atualizar</button> 
                        <button type="button" class="btn btn-outline-danger" onclick='DeletarVendasPeloId(${venda[indice].vendaID})'>Deletar</button></td>
                        </tr >`;
        }
        tabela += '</tbody>';
        tabela += '</table>';
        divTab.innerHTML = tabela;
    }

}


$("#novaVenda").click(function () {
    TitMoodal("Cadastro simples de recorde de vendas");
    MostraMoodal();
    limpaMoodal();//limpa a modal
    $('#vendasID').val(0);
});


function TitMoodal(texto) {
    $(".moodal-title").text(texto);
}

function MostraMoodal() {
    new bootstrap.Modal($("#moodal"), {}).show();
}

function limpaMoodal() {
    $(".vendadt").val('');
    $(".vendadt").removeClass('is-valid');

    $(".venval").val('');
    $(".venval").removeClass('is-valid');

}



function criaVendas(venda) {

    $.ajax({
        url: "/Vendas/novaVenda",
        method: "POST",
        data: {
            venda: venda
        },
        success: function (result) {
            $("#moodal").modal('hide');
            carregaDado();
        }

    });
}

function pegarVendasId(vendaId) {
    $.ajax({
        url: "/Vendas/pegarVendaPeloID",
        method: "GET",
        data: {
            vendaId: vendaId
        },
        success: function (result) {
            console.log(vendaId);
            MostraMoodal(result);
            TitMoodal(`Atualizar a venda de id ${result.vendaID}`);
            $(".vendasID").val(result.vendaID);
            $(".vendadt").val(result.vendaData);
            $(".venval").val(result.vendaValor);

        }
    });
}


function atualizaVendas(venda) {
    $.ajax({
        url: "/Vendas/atualizarVenda",
        method: "POST",
        data: {
            venda: venda
        },
        success: function (vendedorAtualizada) {
            $("#moodal").modal('hide');
            var linhaTabela = $(`#${vendedorAtualizada.vendaID}`);
            carregaDado();
            limpaMoodal();
        }
    });
}



function DeletarVendasPeloId(vendaId) {
    $.ajax({
        url: "/Vendas/deletarVenda",
        method: "POST",
        data: {
            vendaId: vendaId
        },
        success: function (status) {
            if (status) {
                $('.close').alert();
                document.getElementById(vendaId).remove();
            }
            else {
                $('.close').alert();
            }
        }

    });

}

function valForm(VendaData, VendaValor) {
    let DtValida = DataVal(VendaData);
    let ValValido = VendaVal(VendaValor);
    if (DtValida == true && ValValido == true) {
        return true;
    }
    return false;
}
function DataVal(VendaData) {
    //console.log(VendedorNome);
    if (VendaData.trim() == '' || VendaData == undefined || VendaData.length < 10) {
        $(".VendaData").addClass('is-invalid');
        $(".errovendadt").text("Preencha corretamente a data de acordo com DD/MM/YYYY.");
        $(".errovendadt").removeClass("d-none");
        return false;
    } else if (VendaData.length > 30) {
        $(".VendaData").addClass('is-invalid');
        $(".errovendadt").text("Excedeu o limite de 30 caracteres");
        $(".errovendadt").removeClass("d-none");
        return false
    } else {
        $(".VendaData").removeClass('is-invalid');
        $(".VendaData").addClass('is-valid');
        $(".errovendadt").addClass("d-none");
        return true;
    }
}
function VendaVal(VendaValor) {
    //console.log(VendedorNome);errovenval
    if (VendaValor == undefined || VendaValor.length < 1) {
        $(".VendaValor").addClass('is-invalid');
        $(".errovenval").text("Preencha corretamente o valor de venda.");
        $(".errovenval").removeClass("d-none");
        return false;
    } else if (VendaValor.length > 30) {
        $(".VendaValor").addClass('is-invalid');
        $(".errovendadt").text("Excedeu o limite de 30 caracteres");
        $(".errovendadt").removeClass("d-none");
        return false
    } else {
        $(".VendaValor").removeClass('is-invalid');
        $(".VendaValor").addClass('is-valid');
        $(".errovendadt").addClass("d-none");
        return true;
    }
}
