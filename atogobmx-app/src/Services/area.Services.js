import axios from "axios";

export default function AreaServices() {
  const getAreas = (callback) => {
    axios.get("https://localhost:7065/api/Areas").then((response) => {
      callback(response.data);
    });
  };
  return {
    getAreas,
  };
}
