<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MedicoResumen.aspx.cs" Inherits="Presentacion.MenuMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="mb-4">
            <h1 class="display-5">Buenos días Dr.
                <asp:Label ID="lblNombreMedico" runat="server" Text="Label"></asp:Label>!</h1>
            <p>Resumen de tu jornada.</p>
        </div>

        <div class="row">
            <div class="col-md-8">
                <%--Card con borde y sombra que la columna izquierda--%>
                <div class="card shadow-sm border-0 mb-3">

                    <div class="row mt-3 ms-3 me-2">
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

                                <asp:BoundField HeaderText="Observaciones" DataField="Observaciones" />

                                <asp:TemplateField HeaderText="Acciones">

                                    <ItemTemplate>
                                        <asp:LinkButton runat="server"
                                            CommandName="VerHC"
                                            CommandArgument='<%# Eval("Paciente.Id") %>'
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
                <%--Card con borde y sombra que la columna derecha--%>
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">
                        <h5 class="card-title fs-5">Resumen del día </h5>
                        <hr />
                        <p class="card-text">Pacientes atendidos: 2/10</p>
                        <p class="card-text">Resultados pendientes: 3</p>
                        <p class="card-text">...</p>
                        <p class="card-text">...</p>
                        <p class="card-text">...</p>

                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
