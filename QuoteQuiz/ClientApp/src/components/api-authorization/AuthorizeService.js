export class AuthorizeService {
    _user = null;
    _isAuthenticated = false;

    async isAuthenticated() {
        const user = await this.getUser();
        return !!user;
    }

    async getUser() {
        if (this._user && this._user.id) {
            return this._user.id;
        }

        await this.ensureUserManagerInitialized();
        return this._user.id;
    }

    async userSimpleAuth(data) {

        const response = await fetch('api/userManagment/UserSimpleAuth',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data.userID),
            }
        );
        const resultData = await response.json();

        if (resultData) {
            this.updateState(resultData);
        } else {
            this.updateState(undefined);
        }
    }

    async userLogOut() {
        const resultData = await fetch('api/userManagment/UserLogOut',
            {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
            }
        );
        if (resultData) {
            this.updateState(undefined);
        }

    }

    updateState(user) {
        this._user = user;
        this._isAuthenticated = !!this._user;
    }

    async ensureUserManagerInitialized() {

        let response = await fetch("api/UserManagment/GetUserAuthInfo");
        if (!response) {
            this.updateState(undefined);
            throw new Error(`Could not load settings`);
        }

        var userResponse = await response.json();
        this.updateState(userResponse);
    }

    static get instance() { return authService }
}

const authService = new AuthorizeService();

export default authService;

