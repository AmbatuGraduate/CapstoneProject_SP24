import React, { useEffect } from "react";

export const Manage = () => {
  // useEffect(() => {
  //   const fetchData = async () => {
  //     try {
  //       // Make your API call here
  //       const response = await fetch("https://localhost:7024/api/Calendar/AutoAddCalendarEvent",
  //       {
  //         method: "GET",
  //         headers: {
  //           "Content-Type": "application/json",
  //           "Access-Control-Allow-Origin": "*",
  //           "Access-Control-Allow-Credentials": "true",
  //         },
  //         credentials: "include",
  //       });
  //       const data = await response.json();
  //       console.log(data); // Process the API response data as needed
  //     } catch (error) {
  //       console.error("Error fetching data:", error);
  //     }
  //   };

  //   // Call the fetchData function initially
  //   fetchData();

  //   // // Set up the interval to make the API call every 5 seconds (adjust as needed)
  //   // const intervalId = setInterval(fetchData, 600000 );

  //   // // Clean up the interval when the component unmounts
  //   // return () => clearInterval(intervalId);
  // }, []);

  return <div>Manage</div>;
};