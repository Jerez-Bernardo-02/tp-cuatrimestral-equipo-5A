<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PacienteTurnos.aspx.cs" Inherits="Presentacion.PacienteTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top: 40px;">

        <h2 class="mb-5">Mis Turnos</h2>

        <div class="row">
            <div class="col-12">
                <asp:GridView runat="server" ID="dgvPacienteTurnos" AutoGenerateColumns="False" CssClass="table table-striped" OnSelectedIndexChanged="dgvTurnos_SelectedIndexChanged" OnPageIndexChanging="dgvTurnos_PageIndexChanging" AllowPaging="true" PageSize="10">
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

        <div>
            <asp:Button ID="btnVolver" runat="server" Text="Volver al Menu" CssClass="btn btn-primary" OnClick="btnVolver_Click" />
        </div>

    </div>
</asp:Content>

