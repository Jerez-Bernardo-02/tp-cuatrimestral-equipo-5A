<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecepcionistaTurnos.aspx.cs" Inherits="Presentacion.RecepcionistaTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top: 20px;">

        <header class="d-flex justify-content-between align-items-center pb-3 mb-4 border-bottom">
            <h1 class="h1">Gestión de Turnos</h1>

            <div class="d-flex gap-2">
                <%--Botón para Agregar Pacientes--%>
                <asp:LinkButton runat="server" ID="btnNuevoPaciente" CssClass="btn btn-primary" OnClick="btnNuevoPaciente_Click">
                            <i class="bi bi-plus-lg"></i>
                            Nuevo Paciente
                </asp:LinkButton>

                <%--Botón para Agregar Turnos--%>
                <asp:LinkButton runat="server" ID="btnNuevoTurno" CssClass="btn btn-success" OnClick="btnNuevoTurno_Click">
                            <i class="bi bi-plus-lg"></i>
                            Nuevo Turno
                </asp:LinkButton>
            </div>

        </header>

    </div>

    <div class="card">
        <div class="card-body">

            <%--Filtros--%>
            <div class="row">

                <%--Filtro por Fecha--%>
                <div class="col-2">
                    <div class="mb-3">
                        <asp:Label Text="Fecha" runat="server" />
                        <asp:TextBox runat="server" ID="txtFecha" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <%--Filtro por Dni del Paciente--%>
                <div class="col-2">
                    <div class="mb-3">
                        <asp:Label Text="Dni del paciente" runat="server" />
                        <asp:TextBox runat="server" ID="txtDni" CssClass="form-control" PlaceHolder="Ej: 12345678"></asp:TextBox>
                    </div>
                </div>

                <%--Filtro por Especialidad--%>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Especialidad" runat="server" />
                        <asp:DropDownList runat="server" ID="ddlEspecialidad" CssClass="form-select">
                            <asp:ListItem Text="Especialidad" />
                        </asp:DropDownList>
                    </div>
                </div>

                <%--Botón para Filtrar--%>
                <div class="col-2">
                    <div class="mb-3 mt-4">
                        <asp:Button runat="server" ID="btnFiltrar" Text="Filtrar" CssClass="btn btn-primary w-50" OnClick="btnFiltrar_Click" />
                    </div>
                </div>

            </div>

            <%--Listado de turnos--%>
            <asp:GridView runat="server" ID="dgvTurnos" AutoGenerateColumns="False" CssClass="table table-hover text-center" OnSelectedIndexChanged="dgvTurnos_SelectedIndexChanged" OnPageIndexChanging="dgvTurnos_PageIndexChanging" AllowPaging="true" PageSize="10">
                <Columns>

                    <%--Columna de Fecha--%>
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--Columna de Hora--%>
                    <asp:TemplateField HeaderText="Hora">
                        <ItemTemplate>
                            <%# Eval("Fecha", "{0:HH:mm}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--Columna de Dni del Paciente--%>
                    <asp:TemplateField HeaderText="Dni del Paciente">
                        <ItemTemplate>
                            <%# Eval("Paciente.Dni") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--Columna con Nombre y Apellido del Paciente--%>
                    <asp:TemplateField HeaderText="Paciente">
                        <ItemTemplate>
                            <%# Eval("Paciente.Apellido") + " " + Eval("Paciente.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--Columna con Nombre y Apellido del Medico--%>
                    <asp:TemplateField HeaderText="Médico">
                        <ItemTemplate>
                            <%# Eval("Medico.Apellido") + ", " + Eval("Medico.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--Columna con el Estado del Turno--%>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# Eval("Estado.Descripcion") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--Columna de Acciones--%>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkBtnModificarTurno" Text="Editar" CssClass="btn btn-warning btn-sm" ToolTip="Modificar Turno">
                                <i class="bi bi-pencil"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
