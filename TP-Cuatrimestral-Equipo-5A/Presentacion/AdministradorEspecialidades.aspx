<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdministradorEspecialidades.aspx.cs" Inherits="Presentacion.AdministradorEspecialidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="container mt-4">

    <h3 class="fw-bold mb-4">Gestión Especialidades</h3>

    <div class="card shadow-sm">
        <div class="card-body">

            <div class="table-responsive">
                <asp:GridView runat="server" ID="dgvEspecialidades" DataKeyNames="Id" AutoGenerateColumns="False" CssClass="table table-striped align-middle"  AllowPaging="true" PageSize="10" OnSelectedIndexChanged="dgvEspecialidades_SelectedIndexChanged" OnPageIndexChanging="dgvEspecialidades_PageIndexChanging">

                    <Columns>

                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                            <HeaderStyle HorizontalAlign="Center" />

                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkBtnModificarDatos" 
                                    CssClass="btn btn-warning btn-sm d-flex align-items-center justify-content-center gap-1"
                                    CommandName="Select" ToolTip="Modificar datos">

                                    <i class="bi bi-pencil-fill"></i> 
                                    Editar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>

        </div>
    </div>

    <div class="mt-4 d-flex justify-content-between">
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary"/>
        <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="btn btn-outline-secondary" />
    </div>

</div>

</asp:Content>
