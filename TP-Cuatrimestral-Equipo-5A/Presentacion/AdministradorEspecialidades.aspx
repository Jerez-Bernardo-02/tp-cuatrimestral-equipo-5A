<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdministradorEspecialidades.aspx.cs" Inherits="Presentacion.AdministradorEspecialidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

        <h3 class="fw-bold mb-4">Especialidades </h3>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView runat="server" ID="dgvEspecialidades" DataKeyNames="Id" AutoGenerateColumns="False" CssClass="table table-striped"  OnSelectedIndexChanged="dgvEspecialidades_SelectedIndexChanged">
                     <Columns>
                        <asp:BoundField HeaderText="Descripcion" Datafield="Descripcion"/>
                      
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkBtnModificarDatos" Text="Editar" CssClass="btn btn-warning btn-sm" CommandName="Select" ToolTip="Modificar datos">
                                    <i class="bi bi-pencil-fill"></i>
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
