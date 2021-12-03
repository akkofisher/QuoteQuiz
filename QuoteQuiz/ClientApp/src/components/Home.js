import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Quote Quiz!</h1>
        <p>To run application Change Connection string and run migration database.</p>
        <p> run a command at package manager console "UPDATE-DATABASE".</p>
        <p> or Restore database "QuoteQuizDB.bak" included in project.</p>
        <p>   ASP.NET Core 3.1</p>
        <p>  EF Core Entity</p>
        <p>  ReactJS 16.0.0</p>
        <img src="https://i.ibb.co/rM8BQDm/quizdb.jpg" alt="quote-quiz" width="1000"/>
      </div>
    );
  }
}
