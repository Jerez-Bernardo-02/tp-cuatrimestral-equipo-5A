<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HistoriaClinica.aspx.cs" Inherits="Presentacion.HistoriaClinica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Mensaje Exitoso o Error--%>
    <div class="row">
        <div class="col-12">
            <%-- Label de ERROR (Rojo) --%>
            <asp:Label ID="lblMensajeError" runat="server"
                CssClass="alert alert-danger d-block text-center" Visible="false">
            </asp:Label>

            <%-- Label de ÉXITO (Verde) --%>
            <asp:Label ID="lblMensajeExito" runat="server"
                CssClass="alert alert-success d-block text-center" Visible="false">
            </asp:Label>
        </div>
    </div>


    <%-- ENCABEZADO DATOS PACIENTE --%>
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
    <%-- BARRA DE BÚSQUEDA --%>
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
    <div class="row ms-3 me-3">
        <div class="col-md-8 accordion" id="acordeonHC">
            <asp:Repeater runat="server" ID="repeaterHC">
                <ItemTemplate>
                    <div class="accordion-item">
                        <%--Titulo y boton--%>
                        <h2 class="accordion-header">
                            <button class="accordion-button" type="button"
                                data-bs-toggle="collapse" <%--indica que es un acordeon--%>
                                data-bs-target="#collapse-<%# Eval("Id")%>">
                                <%--vincula el div con el id de la HC para redirigir--%>
                                <strong><%# Eval("Fecha") %></strong>

                                -

                            <%# Eval("Asunto") %>
                            </button>
                        </h2>
                        <%--Contenido--%>
                        <div id="collapse-<%# Eval("Id") %>" class="accordion-collapse collapse" <%--asigna el ID para que el botón lo conozca--%>
                            data-bs-parent="#acordeonHC">
                            <div class="accordion-body">

                                <strong>Descripcion detallada:</strong>
                                <p><%# Eval("Descripcion") %></p>
                                <hr />
                                Atendido por: Dr. <%# Eval("Medico.Nombre") %> <%# Eval("Medico.Apellido") %>
                            (<%# Eval("Especialidad.Descripcion") %>)
                            </div>
                        </div>
                    </div>


                </ItemTemplate>
            </asp:Repeater>
            <%-- Mensaje por si no hay datos --%>
            <asp:Label ID="lblMensaje" runat="server" Text="No se encontraron historias clínicas." Visible="false" CssClass="alert alert-warning d-block mt-3"></asp:Label>
        </div>

        <%-- COLUMNA DERECHA (4): PANEL DE ALTA (Oculto por defecto) --%>
        <div class="col-md-4">
            <asp:Panel ID="pnlAlta" runat="server" Visible="false" CssClass="card shadow-sm">
                <div class="card-title mt-2 text-center">
                    <h5>Nueva Historia Clínica</h5>
                </div>
                <div class="card-body">

                    <div class="mb-3">
                        <label class="form-label">Asunto</label>
                        <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control" MaxLength="80" ></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Médico</label>
                        <asp:TextBox ID="txtMedicoAlta" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Especialidad</label>
                        <asp:TextBox ID="txtEspecialidadAlta" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="180"></asp:TextBox>
                    </div>

                    <div class="d-flex justify-content-center gap-2">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    </div>

                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
