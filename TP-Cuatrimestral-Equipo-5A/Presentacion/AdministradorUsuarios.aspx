<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdministradorUsuarios.aspx.cs" Inherits="Presentacion.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container mt-4">

    <h3 class="fw-bold mb-4">Gestión Usuarios</h3>

    <div class="card shadow-sm">
        <div class="card-body">

            <div class="table-responsive">
                <asp:GridView runat="server" ID="dgvUsuarios" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" CssClass="table table-striped align-middle" DataKeyNames="IdUsuario"  OnPageIndexChanging="dgvUsuarios_PageIndexChanging" OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged" OnRowDataBound="dgvUsuarios_RowDataBound" OnRowCommand="dgvUsuarios_RowCommand">
    <Columns>

        <asp:BoundField HeaderText="DNI" DataField="Dni" />
        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
        <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
        <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
        <asp:CheckBoxField HeaderText="Activo" DataField="ActivoUsuario" />
        <asp:BoundField HeaderText="Rol" DataField="Rol" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lnkBtnModificarDatos" CssClass="btn btn-warning btn-sm d-flex align-items-center justify-content-center gap-1" CommandName="Select" ToolTip="Modificar datos">
                    <i class="bi bi-pencil-fill"></i> 
                     Editar
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Alta/Baja">
            <ItemTemplate>

        <!-- Botón INACTIVAR (solo visible si está ACTIVO) -->
                <asp:LinkButton ID="btnInactivar" runat="server" CommandName="Inactivar" CommandArgument='<%# Eval("IdUsuario") %>' Visible='<%# (bool)Eval("ActivoUsuario") %>' CssClass="btn btn-outline-danger btn-sm" ToolTip="Inactivar">
                    <i class="bi bi-person-exclamation"></i>
                </asp:LinkButton>

        <!-- Botón ACTIVAR (solo visible si está INACTIVO) -->
                <asp:LinkButton ID="btnActivar" runat="server" CommandName="Activar" CommandArgument='<%# Eval("IdUsuario") %>' Visible='<%# !(bool)Eval("ActivoUsuario") %>' CssClass="btn btn-outline-success btn-sm" ToolTip="Activar">
                    <i class="bi bi-person-fill-check"></i>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>

</asp:GridView>
            </div>
            <div class="modal fade" id="modalInactivarUsuario" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <div class="modal-header">
        <h5 class="modal-title fw-bold">Confirmar inactivación</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>

      <div class="modal-body">
        <p>
            Este usuario tiene turnos futuros asociados.<br />
            Si lo inactivás, <strong>todos esos turnos serán cancelados automáticamente.</strong>
        </p>
        <p class="fw-bold mb-0">¿Deseás continuar?</p>
      </div>

      <div class="modal-footer">
        <asp:Button ID="btnModalCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="btnModalCancelar_Click" />

        <asp:Button ID="btnModalConfirmar" runat="server" CssClass="btn btn-danger" Text="Confirmar" OnClick="btnModalConfirmar_Click" />
      </div>

    </div>
  </div>
</div>

        </div>
    </div>

    <div class="mt-4 d-flex justify-content-between">
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary"/>
    </div>

</div>
</asp:Content>
