<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MedicoResumen.aspx.cs" Inherits="Presentacion.MenuMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <h1 class="display-6">Bienvenido Dr.
                <asp:Label ID="lblNombreMedico" runat="server" Text="Label"></asp:Label>!</h1>
        <span class="text-muted">Resumen diario</span>
    </div>

    <div class="row">
        <div class="col-md-8">
            <%--Card con borde y sombra: columna izquierda--%>
            <div class="card shadow-sm border-0 mb-3">

                <div class="row mt-3 ms-3 me-2">
                    <%--Grilla de turnos--%>
                    <asp:GridView ID="dgvTurnos" runat="server"
                        CssClass="table table-hover"
                        AutoGenerateColumns="false"
                        OnRowCommand="dgvTurnos_RowCommand"
                        DataKeyNames="Id">
                        <Columns>
                            <asp:BoundField HeaderText="Fecha y hora" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />

                            <asp:TemplateField HeaderText="Paciente">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Paciente.Nombre") + " " + Eval("Paciente.Apellido") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Especialidad" DataField="Especialidad.Descripcion" />

                            <asp:BoundField HeaderText="Observaciones" DataField="Observaciones" />

                            <asp:BoundField HeaderText="Estado" DataField="Estado.Descripcion" />


                            <asp:TemplateField HeaderText="Acciones">

                                <ItemTemplate>
                                    <asp:LinkButton runat="server"
                                        CommandName="VerHC"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-info btn-sm"
                                        ToolTip="Ver Historia Clínica">
                                            <i class="bi bi-eye-fill"></i>
                                    </asp:LinkButton>

                                    <asp:LinkButton runat="server"
                                        CommandName="ModificarEstado"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-warning btn-sm"
                                        ToolTip="Modificar Estado / Ver Observaciones">
                                            <i class="bi bi-pencil-fill"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>

        <div class="col-md-4">
            <%--Card con borde y sombra: columna derecha--%>
            <div class="card shadow-sm border-0 mb-3">
                <div class="card-body">
                    <h5 class="card-title fs-5">Resumen del día </h5>
                    <hr />
                    <%-- Total de Turnos --%>
                    <div class="d-flex justify-content-between">
                        <span class="text-muted">Total Turnos:</span>
                        <span class="text-muted">
                            <asp:Label ID="lblTotalTurnos" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>
                    <hr />

                    <%-- Atendidos --%>
                    <div class="d-flex justify-content-between mb-2">

                        <span class="text-success">Atendidos</span>
                        <span class="text-success">
                            <asp:Label ID="lblAtendidos" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>
                    <%-- Pendientes  --%>
                    <div class="d-flex justify-content-between mb-2">
                        <span class="text-primary">Pendientes</span>
                        <span class="text-primary">
                            <asp:Label ID="lblPendientes" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>

                    <%-- Cancelados --%>
                    <div class="d-flex justify-content-between">
                        <span class="text-danger">Cancelados</span>
                        <span class="text-danger">
                            <asp:Label ID="lblCancelados" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>
