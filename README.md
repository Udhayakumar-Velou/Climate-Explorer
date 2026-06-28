# 🌍 Climate Explorer

An interactive climate visualization application developed using **Unity 6**. The application generates a world map from GeoJSON data and allows users to explore historical climate datasets such as Population, CO₂ Emissions, Temperature, and Renewable Energy using an interactive interface.

---

# 📖 Project Overview

Climate Explorer is designed to visualize historical climate information for countries around the world in an interactive way.

Instead of viewing data in spreadsheets or charts, users can explore climate indicators directly on an interactive world map.

Users can:

- Select different climate datasets
- Change the year using a slider
- Click on any country to view detailed information
- Compare countries visually through dynamic color mapping

---

# ✨ Features

- 🌍 Interactive Flat World Map
- 🖱️ Country Selection
- 🎨 Dynamic Country Coloring
- 📅 Interactive Year Slider
- 📊 Multiple Climate Datasets
- 📋 Country Information Panel
- 🔄 Real-time Data Updates
- 🔍 Camera Zoom and Navigation
- ⚙️ Procedural Mesh Generation

---

# 📊 Supported Climate Datasets

The application currently supports the following datasets:

### Population
Displays the estimated population of each country.

### CO₂ Emissions
Displays CO₂ emissions per person (tons/person).

### Temperature
Displays annual average temperature anomaly (°C).

### Renewable Energy
Displays the percentage of renewable energy consumption.

---

# 🏗️ System Architecture

The project consists of several independent systems.

## 1. GeoJSON Manager

Loads and parses the GeoJSON file containing:

- Country Names
- ISO Codes
- Polygon Coordinates
- MultiPolygon Coordinates

These coordinates are later converted into Unity meshes.

---

## 2. Procedural Flat Map Generation

Unlike importing a ready-made world map, this project generates every country dynamically.

```
GeoJSON File
      │
      ▼
Polygon Coordinates
      │
      ▼
LibTessDotNet Triangulation
      │
      ▼
Unity Mesh
      │
      ▼
Mesh Renderer
      │
      ▼
Interactive Country
```

Each country becomes its own GameObject with:

- Mesh
- Mesh Renderer
- Mesh Collider
- Country Behaviour Script

This enables individual country interaction.

---

## 3. Climate Data Manager

Loads climate datasets from CSV files during application startup.

Datasets include:

- Population
- CO₂ Emissions
- Temperature
- Renewable Energy

Each dataset is stored using nested dictionaries.

```
ISO Code
    │
    ▼
Year
    │
    ▼
Climate Value
```

Example:

```
USA
 ├── 1960 → 180,671,000
 ├── 1970 → 205,052,000
 ├── 1980 → 227,225,000
```

This allows fast retrieval of climate values.

---

## 4. Country Color Manager

Responsible for updating country colors.

Workflow:

```
Selected Dataset
        │
        ▼
Current Year
        │
        ▼
Retrieve Country Value
        │
        ▼
Normalize Data
        │
        ▼
Generate Color
        │
        ▼
Update Country Material
```

Whenever the user changes:

- Dataset
- Year

all countries are recolored automatically.

---

## 5. Year Manager

Controls the year slider.

Responsibilities:

- Update current year
- Refresh country colors
- Refresh selected country information

If a dataset does not contain the selected year, the nearest available year is automatically used.

---

## 6. Country Selection

When the user clicks a country:

```
Mouse Click
      │
      ▼
Raycast
      │
      ▼
Country Behaviour
      │
      ▼
Country UI Manager
      │
      ▼
Information Panel
```

The information panel displays:

- Country Name
- Population
- CO₂ Emissions
- Temperature
- Renewable Energy

---

# 🌐 Flat Map Generation

The world map is generated procedurally from GeoJSON data.

Advantages:

- Lightweight
- Easy to update
- Individual country interaction
- No external 3D models required
- Fully generated inside Unity

---

# 🌎 Globe Prototype

The project initially started as a **3D Globe Visualization**.

A globe mesh generator was developed that projected GeoJSON coordinates onto a sphere.

Although functional, the globe was kept as a prototype while the flat map became the primary visualization because it offers:

- Better country visibility
- Easier interaction
- Clearer comparison between countries
- Better data presentation

The globe scene remains included for future development.

---

# 🛠️ Technologies Used

- Unity 6 LTS
- C#
- GeoJSON
- CSV Climate Datasets
- LibTessDotNet
- TextMesh Pro
- Unity UI
- Universal Render Pipeline (URP)

---

# 📂 Project Structure

```
Assets
│
├── Data
│   ├── Climate CSV Files
│   └── GeoJSON Map
│
├── Scenes
│   ├── FlatMapScene
│   └── GlobeScene
│
├── Scripts
│   ├── Camera
│   ├── Climate
│   ├── Interaction
│   ├── Managers
│   ├── Mesh
│   ├── Models
│   └── Parsers
│
├── Textures
│
└── Settings
```

---

# 🎮 Controls

| Action | Control |
|---------|----------|
| Rotate Map | Left Mouse Drag |
| Pan Map | Right Mouse Drag |
| Zoom | Mouse Scroll |
| Select Country | Left Mouse Click |
| Change Dataset | Dropdown |
| Change Year | Slider |

---

# 🚀 Future Improvements

- Interactive 3D Globe
- Country Search
- Country Border Highlight
- Climate Heatmap Legend
- Country Comparison
- Climate Trend Graphs
- Additional Climate Indicators
- Animated Climate Timeline

---

# 👨‍💻 Team

- Udhayakumar Velou
- Bhavan Vasu
- Kishore Saravanan


## Project Resources


### Google Drive
https://drive.google.com/drive/folders/1ExXKrNeOU9EnVQJ3HcZullh2hZaSG3TH?usp=sharing

The Google Drive folder contains:
- Final Project Report (PDF)
- Demonstration Video (.mov)
- Unity Project Source Code (.zip)

Master's Student

Climate Explorer Project

Unity • C# • GeoJSON • Climate Data Visualization
