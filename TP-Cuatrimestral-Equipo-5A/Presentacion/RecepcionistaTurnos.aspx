<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecepcionistaTurnos.aspx.cs" Inherits="Presentacion.RecepcionistaTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top: 40px;">

        <h2 class="mb-5">Turnos registrados</h2>

        <div class="row mb-3">
            <div class="col-md-2">
                <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtDni" CssClass="form-control" PlaceHolder="Dni del Paciente"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Estado" Value="" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Especialidad" Value="" />
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:Button runat="server" ID="btnFiltrar" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click"/>
            </div>
            <div class="col-md text-end">
                <asp:Button runat="server" ID="btnNuevoTurno" Text="Nuevo Turno" CssClass="btn btn-success" OnClick="btnNuevoTurno_Click" />
            </div>

        </div>

        <div class="row">
            <div class="col-12">
                <asp:GridView runat="server" ID="dgvTurnos" AutoGenerateColumns="False" CssClass="table table-striped" OnSelectedIndexChanged="dgvTurnos_SelectedIndexChanged" OnPageIndexChanging="dgvTurnos_PageIndexChanging" AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:TemplateField HeaderText="Fecha">
                            <ItemTemplate>
                                <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Horario">
                            <ItemTemplate>
                                <%# Eval("Fecha", "{0:HH:mm}") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dni del Paciente">
                            <ItemTemplate>
                                <%# Eval("Paciente.Dni") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paciente">
                            <ItemTemplate>
                                <%# Eval("Paciente.Nombre") + " " + Eval("Paciente.Apellido") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Médico">
                            <ItemTemplate>
                                <%# Eval("Medico.Apellido") + ", " + Eval("Medico.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("Estado.Descripcion") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkBtnModificarTurno" Text="Editar" CssClass="btn btn-warning btn-sm" ToolTip="Modificar Turno">
                                            <i class="bi bi-pencil-fill"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
