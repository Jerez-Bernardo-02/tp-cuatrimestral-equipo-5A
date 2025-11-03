<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecepcionistaPacientes.aspx.cs" Inherits="Presentacion.RecepcionistaPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid mt-3">

        <h3>Pacientes Registrados</h3>

        <div class="row mt-5 mb-3">

            <div class="col-1">
                <asp:TextBox runat="server" ID="txtDNI" CssClass="form-control" Placeholder="Dni"></asp:TextBox>
            </div>

            <div class="col-1">
                <asp:Button runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" />
            </div>

        </div>

        <div class="row">
            <div class="col-10">

                 <!-- Si el Usuario es "MEDICO", solo mostrar PACIENTES asociados -->
                <asp:GridView runat="server" ID="dgvPacientes" AutoGenerateColumns="False" CssClass="table table-light table-hover text-center shadow-sm rounded">
                    <Columns>
                        <asp:BoundField DataField="Documento" HeaderText="Dni" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <!-- Si el usuario es "MEDICO", OCULTAR TODOS los botones -->
                                <!-- Si el usuario es "RECEPCIONISTA", OCULTAR boton de ELIMINAR -->
                                <!-- Si el usuario es "ADMINISTRADOR", MOSTRAR TODOS los botones -->
                                <asp:Button runat="server" Text="Editar" CssClass="btn btn-sm btn-secondary" />
                                <asp:Button runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                <div class="text-end">
                    <asp:Button runat="server" ID="btnNuevoTurno" Text="Nuevo Paciente" CssClass="btn btn-success" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>
