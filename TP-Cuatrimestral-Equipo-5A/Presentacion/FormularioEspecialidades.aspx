<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioEspecialidades.aspx.cs" Inherits="Presentacion.FormularioEspecialidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-4">
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
        <h3 class="ms-3 mb-3">Formulario de Especialidad</h3>
        <h4 class="text-primary ms-3 mb-4">Datos de especialidad</h4>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" placeholder="Descripción de la especialidad"></asp:TextBox>
               
            </div>
        </div>

<!-- Bloque botones -->
<div class="row mb-3 d-flex justify-content-between align-items-start">

    
    <div class="col-md-6 d-flex align-items-center gap-2">

        <!-- Agregar / Guardar -->
        <asp:Button Text="Registrar" ID="btnRegistrar" runat="server" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />

        <!-- Botón Eliminar -->
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" Visible="false" />

    </div>
    <!-- Volver -->
    <div class="col-md-3 text-end">
        <asp:Button Text="Volver" ID="btnVolver" runat="server" CssClass="btn btn-outline-secondary" OnClick="btnVolver_Click" />
    </div>
</div>
<!-- confirmacion eliminacion -->
<div class="row mb-3">
    <div class="col-md-6 d-flex flex-column gap-2">

        <% if (ConfirmaEliminacion) { %>
            <asp:CheckBox Text="Confirmar Eliminación" ID="chkConfirmaEliminacion" runat="server" />
            <asp:Button ID="btnConfirmaEliminar" runat="server" Text="Eliminar" OnClick="btnConfirmaEliminar_Click" CssClass="btn btn-outline-danger" Visible="true" />
        <% } %>
    </div>
</div>
        <div class="row mb-3">
            <div class="col-md-12">
                <asp:Panel ID="pnlResultado" runat="server" Visible="false" CssClass="alert alert-success text-center mt-3">
                    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
                </asp:Panel>
                  <div class="col-md-12">
                     <asp:Panel ID="PanelEliminacion" runat="server" Visible="false" CssClass="alert alert-success text-center mt-3">
                     <asp:Label ID="lblEliminacion" runat="server" Text=""></asp:Label>
                     </asp:Panel>
                 </div>
           </div>
       </div>

</div>
</asp:Content>
