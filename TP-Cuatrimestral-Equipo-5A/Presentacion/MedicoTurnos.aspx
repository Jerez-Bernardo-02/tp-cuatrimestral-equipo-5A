<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MedicoTurnos.aspx.cs" Inherits="Presentacion.Turnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" style="padding-top: 40px;">
        <h2 class="card-title mb-5">Gestion de turnos</h2>


        <div class="row">
            <div class="col-md-12">

                <%--Fila de filtros--%>
                <div class="row mt-4 ms-3">
                    <%--Filtro Nombre--%>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtDniPaciente" CssClass="form-control" Placeholder="DNI"></asp:TextBox>
                    </div>

                    <%--Filtro Nombre--%>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtNombrePaciente" CssClass="form-control" Placeholder="Nombre"></asp:TextBox>
                    </div>
                    <%--Filtro Apellido--%>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtApellidoPaciente" CssClass="form-control" Placeholder="Apellido"></asp:TextBox>
                    </div>
                    <%--Filtro Estado--%>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlEstadoTurno" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Estado" Value="" />
                        </asp:DropDownList>
                    </div>
                    <%--Filtro Fecha--%>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtFechaTurno" runat="server" TextMode="Date"
                            CssClass="form-control">
                             </asp:TextBox>

                    </div>

                    <%--Botron aplicar filtros--%>
                    <div class="col-md-1">
                        <asp:Button runat="server" ID="btnFiltrar" Text="Filtrar" CssClass="btn btn-primary w-100"
                            OnClick="btnFiltrar_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-3 ms-3 me-2">
            <div class="col-12">
                <%--Card con borde y sombra para la grilla --%>
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">

                        <%--Grilla de turnos--%>
                        <asp:GridView ID="dgvTurnos" runat="server"
                            CssClass="table table-hover"
                            AutoGenerateColumns="false"
                            OnRowCommand="dgvTurnos_RowCommand"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:BoundField HeaderText="Fecha y hora" DataField="Fecha" />

                                <asp:TemplateField HeaderText="Paciente">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Paciente.Nombre") + " " + Eval("Paciente.Apellido") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Especialidad" DataField="Especialidad.Descripcion" />

                                <asp:BoundField HeaderText="Estado" DataField="Estado.Descripcion" />

                                <asp:TemplateField HeaderText="Acciones">

                                    <ItemTemplate>
                                        <%--Boton ver historia clinica--%>
                                        <asp:LinkButton runat="server"
                                            CommandName="VerHC"
                                            CommandArgument='<%# Eval("Paciente.Id") %>'
                                            CssClass="btn btn-info btn-sm"
                                            ToolTip="Ver Historia Clínica">
                                            <i class="bi bi-eye-fill"></i>
                                        </asp:LinkButton>

                                        <%--Boton modificar estado del turno--%>
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
        </div>
    </div>
</asp:Content>
