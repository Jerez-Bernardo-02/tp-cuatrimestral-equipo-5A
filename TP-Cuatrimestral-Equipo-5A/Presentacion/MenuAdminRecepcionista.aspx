<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MenuAdminRecepcionista.aspx.cs" Inherits="Presentacion.MenuAdminRecepcionista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container mt-4">

        <h3 class="fw-bold mb-4">Recepcionistas</h3>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvRecepcionistas" runat="server" CssClass="table table-striped align-middle text-center"
                    AutoGenerateColumns="false" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Modificar" 
                                    CssClass="btn btn-outline-secondary btn-sm me-2" />

                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                    CssClass="btn btn-outline-danger btn-sm" />
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
