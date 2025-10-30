<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMedicos.Master" AutoEventWireup="true" CodeBehind="MedicoTurnos.aspx.cs" Inherits="Presentacion.Turnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">

        <div class="row align-items-center">
            <%--Columna izquierda--%>

            <div class="col-md-10">
                <h3 class="mb-0 mt-4 ">Gestión de turnos</h3>
                <p class="lead text-muted mb-3">Visualice y gestione todos sus turnos.</p>
            </div>

            <%--Columna derecha--%>

            <div class="col-md-2">
                <button type="button" class="btn btn-primary">Nuevo turno</button>

            </div>

        </div>

        <%--Fila de filtros--%>
        <div class="row mt-4 ms-3">
            <div class="col-md-2">
                <label class="form-label d-block">Buscar paciente</label>
                <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="form-control" placeholder="Nombre del paciente...">
                </asp:TextBox>

            </div>

            <div class="col-md-2">
                <label class="form-label d-block">Filtrar por médico</label>
                <asp:DropDownList ID="ddlMedicos" runat="server" CssClass="form-select">
                    <asp:ListItem Value="">Todos los médicos</asp:ListItem>
                    <asp:ListItem Value="1">Todavía no hay médicos :(</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-2">
                <label class="form-label d-block">Filtrar por fecha</label>
                <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control"> </asp:TextBox>
            </div>

            <div class="col-md-2">
                <label class="form-label d-block">Filtrar por estado</label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select">
                    <asp:ListItem Value="">Todos los estados</asp:ListItem>
                    <asp:ListItem Value="1">Todavía no hay estados :(</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-2 d-flex align-items-center justify-content-center">
                <button type="button" class="btn btn-outline-info">Aplicar filtros</button>
            </div>

        </div>

        <div class="row mt-3 ms-3 me-2">
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col">FECHA Y HORA</th>
                        <th scope="col">PACIENTE</th>
                        <th scope="col">MÉDICO</th>
                        <th scope="col">ESTADO</th>
                        <th scope="col">ACCIONES</th>

                    </tr>
                </thead>
                <tbody class="table-group-divider">
                    <tr>
                        <th>05-12-2025 10:00</th>
                        <td>Juan Perez</td>
                        <td>Maria Fernandez</td>
                        <td>ACTIVO</td>
                        <td>
                            <button type="button" class="btn btn-primary">Modificar</button>
                        </td>
                    </tr>
                    <tr>
                        <th>06-12-2025 16:00</th>
                        <td>Pedro Perez</td>
                        <td>Maria Fernandez</td>
                        <td>ACTIVO</td>
                        <td>
                            <button type="button" class="btn btn-primary">Modificar</button>
                        </td>
                    </tr>
                    <tr>
                        <th>07-12-2025 11:00</th>
                        <td>Mariela Garcia</td>
                        <td>Maria Fernandez</td>
                        <td>ACTIVO</td>
                        <td>
                            <button type="button" class="btn btn-primary">Modificar</button>
                        </td>
                    </tr>
                    <tr>
                        <th>10-12-2025 13:00</th>
                        <td>Mauro Rodriguez</td>
                        <td>Maria Fernandez</td>
                        <td>ACTIVO</td>
                        <td>
                            <button type="button" class="btn btn-primary">Modificar</button>
                        </td>
                    </tr>
                    <tr>
                        <th>12-12-2025 14:00</th>
                        <td>Juana Gomez</td>
                        <td>Maria Fernandez</td>
                        <td>ACTIVO</td>
                        <td>
                            <button type="button" class="btn btn-primary">Modificar</button>
                        </td>
                    </tr>
                    <tr>
                        <th>12-12-2025 16:00</th>
                        <td>Marcelo Jerez</td>
                        <td>Maria Fernandez</td>
                        <td>ACTIVO</td>
                        <td>
                            <button type="button" class="btn btn-primary">Modificar</button>
                        </td>
                    </tr>

                </tbody>
            </table>

        </div>



    </div>
</asp:Content>
