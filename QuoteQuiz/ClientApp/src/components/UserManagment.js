import React, { Component } from 'react';

export class UserManagment extends Component {
    static displayName = UserManagment.name;

    constructor(props) {
        super(props);
        this.state = { usersData: [], loading: true };
    }

    componentDidMount() {
        this.getUsersData();
    }

    static renderUsersTable(usersData) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Current Mode</th>
                        <th>Deleted</th>
                        <th>Disabled</th>
                        <th>Create Date</th>
                        <th>Modified Date</th>
                    </tr>
                </thead>
                <tbody>
                    {usersData.map(user =>
                        <tr key={user.id}>
                            <td>{user.id}</td>
                            <td>{user.name}</td>
                            <td>{user.email}</td>
                            <td>{user.currentMode}</td>
                            <td>{user.isDeleted}</td>
                            <td>{user.isDisabled}</td>
                            <td>{user.createDate}</td>
                            <td>{user.lastModifiedDate}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : UserManagment.renderUsersTable(this.state.usersData);

        return (
            <div>
                <h1 id="tabelLabel" >User Managment</h1>
                {contents}
            </div>
        );
    }

    async getUsersData() {
        const response = await fetch('api/usermanagment/getusers',
            {
                method: "GET",
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json'
                },
            }
        );
        const data = await response.json();
        this.setState({ usersData: data, loading: false });
    }
}
