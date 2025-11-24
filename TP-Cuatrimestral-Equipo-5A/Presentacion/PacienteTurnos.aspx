<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PacienteTurnos.aspx.cs" Inherits="Presentacion.PacienteTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top: 20px;">

        <h1>Mis Turnos</h1>

        <div class="d-flex justify-content-between align-items-center">
            
            <div>
                <p class="mb-0">
                    Hola <asp:Label runat="server" ID="lblNombrePaciente" Text=""></asp:Label>! Este es tu espacio para gestionar todos tus turnos médicos.
                </p>
            </div>

            <asp:LinkButton runat="server" ID="lnkBtnNuevoTurno" CssClass="btn btn-primary mb-3" OnClick="lnkBtnNuevoTurno_Click">
                <i class="bi bi-plus-lg"></i>
                Nuevo Turno
            </asp:LinkButton>

        </div>

        <hr/>

        <%--Menu--%>
        <ul class="nav nav-tabs mt-3">

            <li class="nav-item">
                <asp:LinkButton Text="Próximos" runat="server" CssClass="nav-link" ID="lnkBtnTurnosProximos" OnClick="lnkBtnTurnosProximos_Click" />
            </li>

            <li class="nav-item">
                <asp:LinkButton Text="Pasados" runat="server" CssClass="nav-link" ID="lnkBtnTurnosPasados" OnClick="lnkBtnTurnosPasados_Click" />
            </li>

            <li class="nav-item">
                <asp:LinkButton Text="Cancelados" runat="server" CssClass="nav-link" ID="lnkBtnTurnosCancelados" OnClick="lnkBtnTurnosCancelados_Click" />
            </li>

        </ul>

        <div class="tab-content border border-top-0 p-4">

            <%--Proximos Turnos--%>
            <asp:Panel runat="server" ID="pnlTurnosProximos">

                <div class="container">
                    <div class="row row-cols-3 g-5">

                        <asp:HiddenField runat="server" ID="hfTurnoCancelar" />
                        <asp:Repeater runat="server" ID="repProximosTurnos">
                            <ItemTemplate>
                                <div class="col">

                                    <%--Tarjeta--%>
                                    <div class="card shadow-sm">

                                        <%--Encabezado--%>
                                        <div class="card-header d-flex justify-content-between">
                                            <div>
                                                <i class="bi bi-clipboard2-pulse"></i>
                                                <asp:Label Text='<%# Eval("Especialidad.Descripcion") %>' runat="server" CssClass="card-title"></asp:Label>
                                            </div>

                                            <asp:Label runat="server" CssClass="fw-bold" Text='<%# Eval("Estado.Descripcion") %>'></asp:Label>
                                        </div>

                                        <%--Cuerpo--%>
                                        <div class="card-body">
                                            <p>
                                                <i class="bi bi-calendar3"></i>
                                                <b>Fecha:</b>
                                                <asp:Label Text='<%# Eval("Fecha", "{0:dd MMMM yyyy, HH:mm tt}") %>' runat="server" CssClass="card-body"></asp:Label>
                                            </p>

                                            <p>
                                                <i class="bi bi-person-vcard"></i>
                                                <b>Médico:</b>
                                                <asp:Label Text='<%# Eval("Medico.NombreCompleto") %>' runat="server" CssClass="card-body"></asp:Label>
                                            </p>

                                        </div>

                                        <%--Pie de Página--%>
                                        <div class="card-footer p-0">

                                            <%--Botón para Cancelar Turno--%>
                                            <asp:LinkButton runat="server" ID="btnCancelarTurno" CssClass="btn btn-danger w-100 rounded-0" CommandArgument='<%# Eval("Id") %>' OnClick="btnCancelarTurno_Click">
                                                <i class="bi bi-x-square"></i>
                                                Cancelar Turno
                                            </asp:LinkButton>

                                        </div>

                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <%--Modal para confirmar cancelación--%>
                        <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Confirmar cancelación</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                                    </div>
                                    <div class="modal-body">
                                        ¿Está seguro que desea cancelar el turno?
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="btnConfirmarCancelar" CssClass="btn btn-danger" OnClick="btnConfirmarCancelar_Click" Text="Cancelar Turno"/>
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Salir</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </asp:Panel>

            <%--Turnos Pasados--%>
            <asp:Panel runat="server" ID="pnlTurnosPasados">
                <div class="row">
                    <asp:GridView runat="server" ID="dgvTurnosPasados" CssClass="table table-hover text-center" ShowHeader="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="dgvTurnosPasados_PageIndexChanging" AutoGenerateColumns="False">

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("Medico.NombreCompleto") + " (" + Eval("Especialidad.Descripcion") + ")" %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("Fecha", "{0:dd MMMM yyyy - hh:mm tt}") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <span class='<%# Eval("Estado.Descripcion").ToString() == "Cerrado" ? "badge bg-success" : "badge bg-warning" %>'>
                                        <%# Eval("Estado.Descripcion") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                </div>
            </asp:Panel>

            <%--Turnos Cancelados--%>
            <asp:Panel runat="server" ID="pnlTurnosCancelados">
                <div class="row">
                    <asp:GridView runat="server" ID="dgvTurnosCancelados" CssClass="table table-hover text-center" ShowHeader="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="dgvTurnosCancelados_PageIndexChanging" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("Medico.NombreCompleto") + " (" + Eval("Especialidad.Descripcion") + ")" %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Eval("Fecha", "{0:dd MMMM yyyy - hh:mm tt}") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <span class="badge bg-danger">
                                        <%# Eval("Estado.Descripcion") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </asp:Panel>

        </div>

    </div>

</asp:Content>

