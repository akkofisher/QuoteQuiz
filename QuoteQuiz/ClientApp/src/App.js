import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { UserManagment } from './components/UserManagment';
import { QuoteManagment } from './components/QuoteManagment';
import { UserQuote } from './components/UserQuote';
import { UserAuthSettings } from './components/UserAuthSettings';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import { Toaster } from 'react-hot-toast';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Toaster />
        <Route exact path='/' component={Home} />
        <Route path='/user-settings' component={UserAuthSettings} />
        {/* <AuthorizeRoute path='/user-quote' component={UserQuote} /> */}
        <Route path='/user-quote' component={UserQuote} />
        <Route path='/quote-managment' component={QuoteManagment} />
        <Route path='/user-managment' component={UserManagment} />
      </Layout>
    );
  }
}
