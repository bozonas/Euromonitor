import React, { useState, useEffect } from 'react';
import { Table, Button } from 'react-bootstrap'
import authService from './api-authorization/AuthorizeService'

export default function BookTable() {
  const [books, setBooks] = useState([]);
  const [authenticated, setAuthenticated] = useState(false);

  useEffect(() => {
    const isAuth = async () => {
      const authenticated = await authService.isAuthenticated();
      setAuthenticated(authenticated);
    }
    isAuth();
    
    const fetchData = async () => {
      const token = await authService.getAccessToken();
      const response = await fetch('api/books', {
        headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
      });
      const data = await response.json();
      setBooks(data);
    };
    fetchData();
  }, []);

  const handleSubscribe = async (id) => {
    const token = await authService.getAccessToken();
    await fetch(`api/books/subscribe/${id}`,
    {
      method: 'POST',
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
    });
    setBooks(prevState => (
      prevState.map(book =>
        book.id === id ? {...book, subscribed: true} : book
      )));
  }

  const handleUnsubscribe = async (id) => {
    const token = await authService.getAccessToken();
    await fetch(`api/books/unsubscribe/${id}`,
    {
      method: 'POST',
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
    });
    setBooks(prevState => (
      prevState.map(book =>
        book.id === id ? {...book, subscribed: false} : book
      )));
  }

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>#</th>
          <th>Book title</th>
          <th>Price</th>
          <th>Subscribe</th>
        </tr>
      </thead>
      <tbody>
        {books.map((book, i) => (
          <tr key={i}>
            <td>{i}</td>
            <td>{book.title}</td>
            <td>{book.price}</td>
            {!authenticated
              ? <td><Button href="#" variant="secondary" disabled>Subscribe</Button></td>
              : book.subscribed ?
                <td><Button onClick={() => handleUnsubscribe(book.id)} variant="danger" >Unsubscribe</Button></td>
                : <td><Button onClick={() => handleSubscribe(book.id)} variant="success" >Subscribe</Button></td>
            }
          </tr>
        ))}
      </tbody>
    </Table>
  );
}

// export class FetchData extends Component {
//   static displayName = FetchData.name;

//   constructor(props) {
//     super(props);
//     this.state = { forecasts: [], loading: true };
//   }

//   componentDidMount() {
//     this.populateWeatherData();
//   }

//   static renderForecastsTable(forecasts) {
//     return (
//       <table className='table table-striped' aria-labelledby="tabelLabel">
//         <thead>
//           <tr>
//             <th>Date</th>
//             <th>Temp. (C)</th>
//             <th>Temp. (F)</th>
//             <th>Summary</th>
//           </tr>
//         </thead>
//         <tbody>
//           {forecasts.map(forecast =>
//             <tr key={forecast.date}>
//               <td>{forecast.date}</td>
//               <td>{forecast.temperatureC}</td>
//               <td>{forecast.temperatureF}</td>
//               <td>{forecast.summary}</td>
//             </tr>
//           )}
//         </tbody>
//       </table>
//     );
//   }

//   render() {
//     let contents = this.state.loading
//       ? <p><em>Loading...</em></p>
//       : FetchData.renderForecastsTable(this.state.forecasts);

//     return (
//       <div>
//         <h1 id="tabelLabel" >Weather forecast</h1>
//         <p>This component demonstrates fetching data from the server.</p>
//         {contents}
//       </div>
//     );
//   }

//   async populateWeatherData() {
//     const token = await authService.getAccessToken();
//     const response = await fetch('weatherforecast', {
//       headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
//     });
//     const data = await response.json();
//     this.setState({ forecasts: data, loading: false });
//   }
// }
