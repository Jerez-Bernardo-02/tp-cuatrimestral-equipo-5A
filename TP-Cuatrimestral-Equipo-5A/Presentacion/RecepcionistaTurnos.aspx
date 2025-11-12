<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecepcionistaTurnos.aspx.cs" Inherits="Presentacion.RecepcionistaTurnos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top: 40px;">
        <h2 class="card-title mb-5">Turnos registrados</h2>

        <div class="row mb-3">
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtDni" CssClass="form-control" PlaceHolder="Dni del Paciente"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtEspecialidad" CssClass="form-control" Placeholder="Especialidad"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Button runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" />
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">

                        <asp:GridView runat="server" ID="dgvTurnos" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Fecha">
                                    <itemtemplate>
                                        <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                                    </itemtemplate>
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
                                <asp:TemplateField HeaderText="Especialidad">
                                    <ItemTemplate>

                                        <%# Eval("Especialidad.Descripcion") %>
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
                <div class="text-end">
                    <asp:Button runat="server" ID="btnNuevoTurno" Text="Nuevo Turno" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
