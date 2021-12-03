import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { UserManagment } from './components/userManagment/UserManagment';
import { QuoteManagment } from './components/quoteManagment/QuoteManagment';
import { UserQuote } from './components/userQuote/UserQuote';
import { UserAuthSettings } from './components/userAuthSettings/UserAuthSettings';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/user-settings' component={UserAuthSettings} />
        <Route path='/user-quote' component={UserQuote} />
        <Route path='/quote-managment' component={QuoteManagment} />
        <Route path='/user-managment' component={UserManagment} />
      </Layout>
    );
  }
}
