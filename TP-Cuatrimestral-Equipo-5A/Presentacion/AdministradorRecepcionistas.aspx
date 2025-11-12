<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdministradorRecepcionistas.aspx.cs" Inherits="Presentacion.MenuAdminRecepcionista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container mt-4">

        <h3 class="fw-bold mb-4">Recepcionistas</h3>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView runat="server" ID="dgvRecepcionistas" AutoGenerateColumns="False" CssClass="table table-striped">
                     <Columns>
                        <asp:BoundField HeaderText="Dni" Datafield="Dni"/>
                        <asp:BoundField HeaderText="Nombre" Datafield="Nombre"/>
                        <asp:BoundField HeaderText="Apellido" Datafield="Apellido"/>
                        <asp:BoundField HeaderText="Email" Datafield="Email"/>
                        <asp:BoundField HeaderText="Telefono" Datafield="Telefono"/>
     
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkBtnModificarDatos" Text="Editar" CssClass="btn btn-warning btn-sm" ToolTip="Modificar datos">
                                    <i class="bi bi-pencil-fill"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lnkBtnVerTurnos"  Text="Turnos" CssClass="btn btn-sm btn-secondary btn-sm" ToolTip="Ver Turnos Asociados">
                                    <i class="bi bi-eye-fill"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
               </asp:GridView>
            </div>
        </div>

        <div class="mt-4 d-flex justify-content-between">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary"/>

            <asp:Button ID="btnVolver" runat="server" Text="Volver" onClick="btnVolver_Click" CssClass="btn btn-outline-secondary" />
        </div>

    </div>
</asp:Content>
