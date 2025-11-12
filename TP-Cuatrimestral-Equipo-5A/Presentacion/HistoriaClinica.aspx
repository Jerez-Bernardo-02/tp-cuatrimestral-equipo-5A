<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HistoriaClinica.aspx.cs" Inherits="Presentacion.HistoriaClinica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4 ms-3">
        <h1 class="display-5">Historia Clínica de 
       
            <asp:Label ID="lblNombrePaciente" runat="server"></asp:Label>
        </h1>
        <p>
            DNI:
            <asp:Label ID="lblDni" runat="server"></asp:Label>
            | 
        Fecha de Nacimiento:
            <asp:Label ID="lblFechaNacimiento" runat="server"></asp:Label>
        </p>
    </div>
    <div class="row mb-3 ms-3">

        <div class="col-md-4">
            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control"
                placeholder="Buscar por: Asunto, descripción, médico..."></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                CssClass="btn btn-info" OnClick="btnBuscar_Click" />
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnNuevaEntrada" runat="server" Text="Añadir Nueva Entrada"
                CssClass="btn btn-primary" OnClick="btnNuevaEntrada_Click" />
        </div>

    </div>

    <div class="accordion ms-3 mt-3 me-3" id="acordeonHC">
        <asp:Repeater runat="server" ID="repeaterHC">
            <ItemTemplate>
                <div class="accordion-item">
                    <%--Titulo y boton--%>
                    <h2 class="accordion-header">
                        <button class="accordion-button" type="button" 
                            data-bs-toggle="collapse"  <%--indica que es un acordeon--%>
                            data-bs-target="#collapse-<%# Eval("Id")%>">  <%--vincula el div con el id de la HC para redirigir--%>
                            <strong> <%# Eval("Fecha") %></strong>
                        </button>
                    </h2>
                    <%--Contenido--%>
                    <div id="collapse-<%# Eval("Id") %>" class="accordion-collapse collapse"  <%--asigna el ID para que el botón lo conozca--%>
                        data-bs-parent="#acordeonHC">
                        <div class="accordion-body">

                            <strong>Descripcion detallada:</strong> 
                            <p> <%# Eval("Descripcion") %></p>
                            <hr />
                            Atendido por: Dr. <%# Eval("Medico.Nombre") %> <%# Eval("Medico.Apellido") %>
                            (<%# Eval("Especialidad.Descripcion") %>)
                        </div>
                    </div>
                </div>


            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
